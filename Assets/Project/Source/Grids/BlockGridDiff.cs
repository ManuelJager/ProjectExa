using System.Collections.Generic;
using Exa.Grids.Blueprints;
using Exa.Ships;
using Exa.UI.Tooltips;

namespace Exa.Grids {
    public class BlockGridDiff : MemberCollectionListener<BlockGrid> {
        private BlueprintGrid target;

        public BlockGridDiff(BlockGrid source, BlueprintGrid target) : base(source) {
            this.target = target;
            PendingAdd = new GridMemberDiffList();
            PendingRemove = new GridMemberDiffList();
            Diff();
        }
        
        public GridMemberDiffList PendingAdd { get; private set; }
        
        public GridMemberDiffList PendingRemove { get; private set; }

        public void TrackNewTarget(BlueprintGrid target) {
            this.target = target;
            Diff();
        }

        public TooltipGroup GetDebugTooltipComponents() {
            return new TooltipGroup(
                1,
                new TooltipText($"Pending add: {PendingAdd.Count} Items"),
                new TooltipText($"Pending remove: {PendingRemove.Count} Items")
            );
        }

        private void Diff() {
            PendingAdd.Clear();
            PendingRemove.Clear();

            foreach (var aBpBlock in target) {
                FilteredAddToPending(source, aBpBlock, PendingAdd);
            }

            foreach (var block in source) {
                FilteredAddToPending(target, block, PendingRemove);
            }
        }

        // Get whether a blueprint block doesn't exist on the target, or the blocks differ
        private static void FilteredAddToPending<T>(Grid<T> grid, IGridMember member, IList<IGridMember> destination)
            where T : class, IGridMember {
            if (!grid.TryGetMember(member.GridAnchor, out var block) || !block.Equals(member)) {
                destination.Add(member);
            }
        }

        protected override void OnMemberAdded(IGridMember member) {
            // Check if the block is pending to be added, if it isn't, mark it as pending to be removed
            if (!PendingAdd.Remove(member)) {
                PendingRemove.Add(member);
            }
        }

        protected override void OnMemberRemoved(IGridMember member) {
            // Check if the block is pending to be removed, if it isn't, mark it as pending to be added
            if (!PendingRemove.Remove(member)) {
                PendingAdd.Add(member);
            }
        }
    }
}