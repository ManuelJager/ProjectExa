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
using Object = UnityEngine.Object;

namespace Exa.Ships
{
    public class BlockGrid : Grid<Block>
    {
        private readonly Transform container;

        public IGridInstance Parent { get; }
        public bool Rebuilding { get; set; }
        public BlockGridMetadata Metadata { get; }

        public BlockGrid(Transform container, IGridInstance parent)
            : base(totals: (parent as GridInstance)?.Totals) {
            this.container = container;

            Parent = parent;
            Metadata = new BlockGridMetadata(GridMembers);
        }

        public override void Add(Block gridMember) {
            if (gridMember.GetIsController() && Controller != null) {
                throw new DuplicateControllerException(gridMember.GridAnchor);
            }

            base.Add(gridMember);
        }

        public override void Remove(Block gridMember) {
            base.Remove(gridMember);

            // Only rebuild if it isn't being rebuilt already
            if (!Rebuilding) {
                GameSystems.BlockGridManager.AttemptRebuild(Parent);
            }
        }

        internal void Import(Blueprint blueprint) {
            foreach (var anchoredBlueprintBlock in blueprint.Blocks) {
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