using System.Collections.Generic;
using System.Diagnostics;
using Exa.Grids.Blueprints;

namespace Exa.Grids {
    public class GridMemberDiffList : List<IGridMember>{
    #if ENABLE_BLOCK_LOGS
        private List<string> Logs { get; } = new List<string>();
    #endif
        
        public new void Add(IGridMember member) {
        #if ENABLE_BLOCK_LOGS
            Logs.Add($"Added {member}, with trace: {new StackTrace()}");
        #endif
            
            base.Add(new ABpBlock(member));
        }

        public new bool Remove(IGridMember member) {
        #if ENABLE_BLOCK_LOGS    
            Logs.Add($"Removed {member}, with trace: {new StackTrace()}");
        #endif
            
            return base.Remove(member);
        }
    }
}