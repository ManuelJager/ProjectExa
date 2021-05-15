using Exa.Grids;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blueprints;
using Exa.UI.Tooltips;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Exa.Ships
{
    public class BlockGrid : Grid<Block>
    {
        private readonly Transform container;
        private GridTotals totals;

        public IGridInstance Parent { get; }
        public bool Rebuilding { get; set; }
        public BlockGridMetadata Metadata { get; }
        public Block Controller { get; protected set; }
        
        public BlockGrid(Transform container, IGridInstance parent) {
            this.container = container;
            this.totals = Systems.Blocks.Totals.StartWatching(this, parent.BlockContext);

            Parent = parent;
            Metadata = new BlockGridMetadata(GridMembers);
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

            base.Add(block);
        }

        public override void Remove(Block block) {
            base.Remove(block);

            if (block.GetIsController()) {
                Controller = null;
            }

            // Only rebuild if it isn't being rebuilt already
            if (!Rebuilding) {
                GameSystems.BlockGridManager.AttemptRebuild(Parent);
            }
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