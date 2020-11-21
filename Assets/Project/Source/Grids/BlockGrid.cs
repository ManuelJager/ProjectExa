using System;
using Exa.Grids;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blueprints;
using Exa.UI.Tooltips;
using Exa.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Ships
{
    public class BlockGrid : Grid<Block>
    {
        private readonly Transform container;
        private readonly Action destroyCallback;

        public IGridInstance Parent { get; }
        public bool Rebuilding { get; set; }
        public BlockGridMetadata Metadata { get; private set; }

        public BlockGrid(Transform container, Action destroyCallback, IGridInstance parent)
            : base(totals: (parent as Ship)?.Totals) {
            this.container = container;
            this.destroyCallback = destroyCallback;

            Parent = parent;
            Metadata = new BlockGridMetadata(GridMembers);
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
                GameSystems.BlockGridManager.AttemptRebuild(Parent);

            return block;
        }

        internal void Import(Blueprint blueprint, BlockContext blockContext) {
            foreach (var anchoredBlueprintBlock in blueprint.Blocks) {
                Add(CreateBlock(anchoredBlueprintBlock, blockContext));
            }
        }

        private Block CreateBlock(AnchoredBlueprintBlock anchoredBlueprintBlock, BlockContext blockContext) {
            var block = anchoredBlueprintBlock.CreateInactiveBlockInGrid(container, blockContext);
            block.Parent = Parent;
            block.gameObject.SetActive(true);
            return block;
        }

        public IEnumerable<ITooltipComponent> GetDebugTooltipComponents() => new ITooltipComponent[] {
        };

        public void DestroyIfEmpty() {
            if (!GridMembers.Any())
                destroyCallback();
        }
    }
}