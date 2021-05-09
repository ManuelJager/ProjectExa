using System.Collections.Generic;
using System.Linq;
using Exa.Grids;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blueprints;
using Exa.Ships;
using Exa.UI.Tooltips;
using UnityEngine;

namespace Project.Source.Grids
{
    public class BlockGridDiff : MemberCollectionListener<BlockGrid>
    {
        private BlueprintGrid target;
        private List<IGridMember> addedToTarget;
        private List<IGridMember> removedFromTarget;

        public IEnumerable<IGridMember> Added => addedToTarget;
        public IEnumerable<IGridMember> Removed => removedFromTarget;
        
        public BlockGridDiff(BlockGrid source, BlueprintGrid target) : base(source) {
            this.target = target;
            addedToTarget = new List<IGridMember>();
            removedFromTarget = new List<IGridMember>();
            Diff();
        }

        public void TrackNewTarget(BlueprintGrid target) {
            this.target = target;
            Diff();
        }

        public TooltipGroup GetDebugTooltipComponents() => new TooltipGroup(1,
            new TooltipText($"Added to Target: {addedToTarget.Count} Items"),
            new TooltipText($"Removed from target: {removedFromTarget.Count} Items")
        );

        private void Diff() {
            addedToTarget.Clear();
            removedFromTarget.Clear();
            
            addedToTarget.AddRange(target.Where(FilterAdd));
            removedFromTarget.AddRange(source.Where(FilterRemoved));
        }

        private bool FilterAdd(ABpBlock aBpBlock) {
            return PassesFilter(source, aBpBlock);
        }

        private bool FilterRemoved(Block block) {
            return PassesFilter(target, block);
        }

        // Get whether a blueprint block doesn't exist on the target, or the blocks differ
        private static bool PassesFilter<T>(Grid<T> grid, IGridMember member) 
            where T : class, IGridMember {
            return !grid.TryGetMember(member.GridAnchor, out var block) || !block.Equals(member);
        }
        
        protected override void OnMemberAdded(IGridMember member) {
            // Check if
            if (!addedToTarget.Remove(member)) {
                removedFromTarget.Add(member);
            }
        }

        protected override void OnMemberRemoved(IGridMember member) {
            if (!removedFromTarget.Remove(member)) {
                addedToTarget.Add(member);
            }
        }
    }
}