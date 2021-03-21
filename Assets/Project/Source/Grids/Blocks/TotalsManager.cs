using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using Exa.Utils;
using Project.Source.Grids;
using UnityEngine;

namespace Exa.Grids.Blocks
{
    // TODO: Add totals invalidation, when the research context of the 
    public class TotalsManager : MonoBehaviour
    {
        private Dictionary<IMemberCollection, Dictionary<BlockContext, TotalsCache>> totalsDictionary;

        private void Awake() {
            totalsDictionary = new Dictionary<IMemberCollection, Dictionary<BlockContext, TotalsCache>>();
        }
        
        public GridTotals StartWatching(IMemberCollection grid, BlockContext context) {
            // TODO: Prevent allocation
            if (totalsDictionary.ContainsKey(grid) && totalsDictionary[grid].ContainsKey(context)) {
                throw new Exception("Grid with given context already being watched");
            }
            
            var cache = new TotalsCache {
                collection = grid,
                totals = new GridTotals(context)
            };
            
            cache.Reset();
            cache.AddListeners();
            
            totalsDictionary.EnsureCreated(grid, () => new Dictionary<BlockContext, TotalsCache>());
            totalsDictionary[grid].Add(context, cache);
            
            return cache.totals;
        }

        public void InvalidateTotals(BlockContext context) {
            IEnumerable<TotalsCache> GetInvalidCaches() {
                foreach (var (cacheContext, cache) in totalsDictionary
                    .SelectMany(blockContextDict => blockContextDict.Value.Unpack())
                ) {
                    if (cacheContext == context) {
                        yield return cache;
                    }
                }
            }
            
            foreach (var cache in GetInvalidCaches()) {
                cache.Reset();
            }
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

        private class TotalsCache
        {
            public IMemberCollection collection;
            public GridTotals totals;

            public void AddListeners() {
                collection.MemberAdded += OnMemberAdded;
                collection.MemberRemoved += OnMemberRemoved;
            }

            public void RemoveListeners() {
                collection.MemberAdded -= OnMemberAdded;
                collection.MemberRemoved -= OnMemberRemoved;
            }

            public void Reset() {
                totals.Reset();
                
                foreach (var member in collection.GetMembers()) {
                    OnMemberAdded(member);
                }
            }

            public void OnMemberAdded(IGridMember member) {
                member.AddGridTotals(totals);
            }

            public void OnMemberRemoved(IGridMember member) {
                member.RemoveGridTotals(totals);
            }
        }
    }
}