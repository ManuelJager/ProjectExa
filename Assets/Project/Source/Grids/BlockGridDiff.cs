using System.Collections.Generic;
using System.Linq;
using Exa.Grids;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blueprints;
using Exa.Ships;
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

        private void Diff() {
            addedToTarget.Clear();
            removedFromTarget.Clear();
            
            addedToTarget.AddRange(target.Where(FilterAdd));
            removedFromTarget.AddRange(source.Where(FilterRemoved));
            
            Debug.Log($"Added to target {addedToTarget.Count}");
            Debug.Log($"Removed from target {removedFromTarget.Count}");
        }

        private bool FilterAdd(ABpBlock aBpBlock) {
            return PassesFilter(source, aBpBlock);
        }

        private bool FilterRemoved(Block block) {
            return PassesFilter(target, block);
        }

        // Get whether a blueprint block doesn't exist on the target, or the blocks differ
        private bool PassesFilter<T>(Grid<T> grid, IGridMember member) 
            where T : class, IGridMember {
            if (!grid.TryGetMember(member.GridAnchor, out var block)) {
                return true;
            }
            
            var equals = !member.Equals(block);

            return equals;
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