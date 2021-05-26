using System;
using Exa.Grids;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blueprints;
using Exa.UI.Tooltips;
using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks.Components;
using Exa.Types;
using Exa.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Exa.Ships
{
    public class BlockGrid : Grid<Block>
    {
        private readonly Transform container;
        private GridTotals totals;
        private DefaultDict<Type, List<BlockBehaviour>> blockBehaviours;

        public IGridInstance Parent { get; }
        public bool Rebuilding { get; set; }
        public Block Controller { get; protected set; }
        
        public BlockGrid(Transform container, IGridInstance parent) {
            this.container = container;
            this.totals = Systems.Blocks.Totals.StartWatching(this, parent.BlockContext);

            blockBehaviours = new DefaultDict<Type, List<BlockBehaviour>>(_ => new List<BlockBehaviour>());
            Parent = parent;
        }

        public GridTotals GetTotals() {
            return totals;
        }
 
        public override void Add(Block block) {
            if (block.GetIsController()) {
                if (Controller != null) {
                    throw new DuplicateControllerException(block.GridAnchor);
                }
                
                Controller = block;
            }

            foreach (var behaviour in block.GetBehaviours()) {
                blockBehaviours[behaviour.GetType()].Add(behaviour);
            }

            base.Add(block);
        }

        public override void Remove(Block block) {
            base.Remove(block);

            if (block.GetIsController()) {
                Controller = null;
            }

            foreach (var behaviour in block.GetBehaviours()) {
                blockBehaviours[behaviour.GetType()].Remove(behaviour);
            }

            // Only rebuild if it isn't being rebuilt already
            if (!Rebuilding) {
                GS.BlockGridManager.AttemptRebuild(Parent);
            }
        }

        public IEnumerable<T> Query<T>() 
            where T : BlockBehaviour {
            return blockBehaviours[typeof(T)].Cast<T>();
        }

        public IEnumerable<T> QueryLike<T>() {
            return blockBehaviours
                .Where(kvp => typeof(T).IsAssignableFrom(kvp.Key))
                .SelectMany(x => x.Value)
                .Cast<T>();
        }

        public IEnumerable<ITooltipComponent> GetDebugTooltipComponents() => new ITooltipComponent[] { };

        public void DestroyIfEmpty() {
            if (!GridMembers.Any()) {
                Object.Destroy(container.gameObject);
            }
        }

        internal void Import(Blueprint blueprint) {
            foreach (var anchoredBlueprintBlock in blueprint.Grid) {
                Place(anchoredBlueprintBlock);
            }
        }
        
        internal void Place(ABpBlock aBpBlock) {
            Add(CreateBlock(aBpBlock));
        }

        internal void Destroy(Vector2Int gridAnchor) {
            GetMember(gridAnchor).DestroyBlock();
        }
        
        private Block CreateBlock(ABpBlock aBpBlock) {
            var block = aBpBlock.CreateInactiveBlockInGrid(container, Parent.BlockContext);
            block.Parent = Parent;
            block.gameObject.SetActive(true);
            return block;
        }
    }
}