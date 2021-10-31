using System.Collections.Generic;
using System.Diagnostics;
using Exa.Grids.Blueprints;

namespace Exa.Grids {
    public delegate void DiffChanged(IGridMember member);
    
    public class GridMemberDiffList : List<IGridMember>{
    #if ENABLE_BLOCK_LOGS
        private List<string> Logs { get; } = new List<string>();
    #endif

        public event DiffChanged MemberAdded;
        public event DiffChanged MemberRemoved;
        
        public new void Add(IGridMember member) {
        #if ENABLE_BLOCK_LOGS
            Logs.Add($"Added {member}, with trace: {new StackTrace()}");
        #endif
            var copy = new ABpBlock(member);
            
            base.Add(copy);
            
            MemberAdded?.Invoke(copy);
        }

        public new bool Remove(IGridMember member) {
        #if ENABLE_BLOCK_LOGS    
            Logs.Add($"Removed {member}, with trace: {new StackTrace()}");
        #endif
            
            var removed = base.Remove(member);
            
            MemberRemoved?.Invoke(member);

            return removed;
        }
    }
}