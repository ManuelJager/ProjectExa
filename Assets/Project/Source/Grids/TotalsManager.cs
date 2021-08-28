using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Debugging;
using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.IO;
using Exa.Ships;
using Exa.Utils;
using UnityEngine;

namespace Exa.Grids {
    public class TotalsManager : MonoBehaviour {
        private Dictionary<IMemberCollection, Dictionary<BlockContext, TotalsCache>> totalsDictionary;

        private void Awake() {
            totalsDictionary = new Dictionary<IMemberCollection, Dictionary<BlockContext, TotalsCache>>();
            S.Research.ResearchChanged += InvalidateTotals;
        }

        public GridTotals StartWatching(IMemberCollection grid, BlockContext context) {
            if (DebugMode.Ships.IsEnabled()) {
                switch (grid) {
                    case BlockGrid blockGrid:
                        Debug.Log(IOUtils.ToJson(new {
                            message = "Started watching block grid",
                            grid = blockGrid.Parent.Transform.name
                        }));

                        break;
                    case BlueprintGrid _:
                        Debug.Log(IOUtils.ToJson(new {
                            message = "Started watching blueprint grid"
                        }));

                        break;
                }
            }
            
            if (totalsDictionary.ContainsKey(grid) && totalsDictionary[grid].ContainsKey(context)) {
                throw new Exception("Grid with given context already being watched");
            }

            var cache = new TotalsCache(grid) {
                totals = new GridTotals(context)
            };

            cache.Reset();
            cache.AddListeners();

            if (!totalsDictionary.ContainsKey(grid)) {
                totalsDictionary.Add(grid, new Dictionary<BlockContext, TotalsCache>() {
                    { context, cache }
                });
            }

            return cache.totals;
        }

        public GridTotals GetGridTotalsSafe(IMemberCollection grid, BlockContext context) {
            return totalsDictionary.Get(grid)?.Get(context) == null
                ? StartWatching(grid, context)
                : GetGridTotals(grid, context);
        }

        public GridTotals GetGridTotals(IMemberCollection grid, BlockContext context) {
            return totalsDictionary[grid][context].totals;
        }

        public void StopWatching(IMemberCollection grid, BlockContext context) {
            totalsDictionary[grid][context].RemoveListeners();
            totalsDictionary[grid].Remove(context);

            if (totalsDictionary[grid].Values.Count == 0) {
                totalsDictionary.Remove(grid);
            }
        }

        private void InvalidateTotals(BlockContext context) {
            IEnumerable<TotalsCache> GetInvalidCaches() {
                foreach (var (cacheContext, cache) in totalsDictionary
                    .SelectMany(blockContextDict => blockContextDict.Value.Unpack())
                ) {
                    if ((cacheContext & context) == cacheContext) {
                        yield return cache;
                    }
                }
            }

            foreach (var cache in GetInvalidCaches()) {
                cache.Reset();
            }
        }

        private class TotalsCache : MemberCollectionListener<IMemberCollection> {
            public GridTotals totals;

            public TotalsCache(IMemberCollection source) : base(source) { }

            public override void Reset() {
                totals.Reset();
                base.Reset();
            }

            protected override void OnMemberAdded(IGridMember member) {
                member.AddGridTotals(totals);
            }

            protected override void OnMemberRemoved(IGridMember member) {
                member.RemoveGridTotals(totals);
            }
        }
    }
}