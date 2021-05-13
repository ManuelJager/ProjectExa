using System.Collections.Generic;
using Exa.Grids;
using Exa.Grids.Blueprints;
using Exa.Ships;
using Exa.UI.Tooltips;
using Exa.Utils;

namespace Project.Source.Grids
{
    public class BlockGridDiff : MemberCollectionListener<BlockGrid>
    {
        private BlueprintGrid target;
        private List<IGridMember> pendingAdd;
        private List<IGridMember> pendingRemove;

        public IEnumerable<IGridMember> PendingAdd => pendingAdd;
        public IEnumerable<IGridMember> PendingRemove => pendingRemove;
        
        public BlockGridDiff(BlockGrid source, BlueprintGrid target) : base(source) {
            this.target = target;
            pendingAdd = new List<IGridMember>();
            pendingRemove = new List<IGridMember>();
            Diff();
        }

        public void TrackNewTarget(BlueprintGrid target) {
            this.target = target;
            Diff();
        }

        public TooltipGroup GetDebugTooltipComponents() => new TooltipGroup(1,
            new TooltipText($"Pending add: {pendingAdd.Count} Items"),
            new TooltipText($"Pending remove: {pendingRemove.Count} Items")
        );

        private void Diff() {
            pendingAdd.Clear();
            pendingRemove.Clear();

            foreach (var aBpBlock in target) {
                FilteredAddToPending(source, aBpBlock, pendingAdd);
            }

            foreach (var block in source) {
                FilteredAddToPending(target, block, pendingRemove);
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
            if (!pendingAdd.Remove(member)) {
                pendingRemove.Add(member);
            }
        }

        protected override void OnMemberRemoved(IGridMember member) {
            // Check if the block is pending to be removed, if it isn't, mark it as pending to be added
            if (!pendingRemove.Remove(member)) {
                pendingAdd.Add(member);
            }
        }
    }
}