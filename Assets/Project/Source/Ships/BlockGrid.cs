using Exa.Grids;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blueprints;
using Exa.UI.Tooltips;
using Exa.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Ships
{
    public class BlockGrid : Grid<Block>
    {
        private readonly Transform container;
        private readonly Ship ship;

        public bool Rebuilding { get; set; }
        public BlockContext BlockContext { get; }

        public Ship Ship => ship;

        public BlockGrid(Transform container, Ship ship, BlockContext blockContext)
            : base(totals: ship?.Totals) {
            this.container = container;
            this.ship = ship;
            BlockContext = blockContext;
        }

        public override void Add(Block gridMember) {
            if (gridMember.GetIsController() && Controller != null) {
                throw new DuplicateControllerException(gridMember.GridAnchor);
            }

            base.Add(gridMember);
        }

        public override Block Remove(Vector2Int key) {
            var block = base.Remove(key);

            // Only mark this grid as dirty if it's not in the process of being rebuilt
            if (!Rebuilding)
                GameSystems.BlockGridManager.MarkDirty(this);

            return block;
        }

        internal void Import(Blueprint blueprint, BlockContext blockContext) {
            foreach (var anchoredBlueprintBlock in blueprint.Blocks) {
                Add(CreateBlock(anchoredBlueprintBlock, blockContext));
            }
        }

        private Block CreateBlock(AnchoredBlueprintBlock anchoredBlueprintBlock, BlockContext blockContext) {
            var block = anchoredBlueprintBlock.CreateInactiveBlockBehaviourInGrid(container, blockContext);
            block.BlockGrid = this;
            block.gameObject.SetActive(true);
            return block;
        }

        public IEnumerable<ITooltipComponent> GetDebugTooltipComponents() => new ITooltipComponent[] {
        };
    }
}