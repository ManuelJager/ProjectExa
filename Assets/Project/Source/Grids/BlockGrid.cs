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

        public override void Add(Block gridMember) {
            if (gridMember.GetIsController()) {
                if (Controller != null) {
                    throw new DuplicateControllerException(gridMember.GridAnchor);
                }
                
                Controller = gridMember;
            }

            base.Add(gridMember);
        }

        public override void Remove(Block gridMember) {
            base.Remove(gridMember);

            if (gridMember.GetIsController()) {
                Controller = null;
            }

            // Only rebuild if it isn't being rebuilt already
            if (!Rebuilding) {
                GameSystems.BlockGridManager.AttemptRebuild(Parent);
            }
        }

        internal void Import(Blueprint blueprint) {
            foreach (var anchoredBlueprintBlock in blueprint.Grid) {
                Add(CreateBlock(anchoredBlueprintBlock));
            }
        }

        private Block CreateBlock(ABpBlock aBpBlock) {
            var block = aBpBlock.CreateInactiveBlockInGrid(container, Parent.BlockContext);
            block.Parent = Parent;
            block.gameObject.SetActive(true);
            return block;
        }

        public IEnumerable<ITooltipComponent> GetDebugTooltipComponents() => new ITooltipComponent[] {
        };

        public void DestroyIfEmpty() {
            if (!GridMembers.Any())
                Object.Destroy(container.gameObject);
        }
    }
}