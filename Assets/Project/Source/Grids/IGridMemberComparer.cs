using System.Collections.Generic;
using Exa.Grids;

namespace Exa.Grids
{
    public class IGridMemberComparer : IEqualityComparer<IGridMember>
    {
        private static IGridMemberComparer defaultComparer;
        public static IGridMemberComparer Default => defaultComparer ??= new IGridMemberComparer();
        
        public bool Equals(IGridMember x, IGridMember y) {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            return x.GridAnchor.Equals(y.GridAnchor) && x.BlueprintBlock.Equals(y.BlueprintBlock);
        }

        public int GetHashCode(IGridMember obj) {
            unchecked {
                return (obj.GridAnchor.GetHashCode() * 397) ^ obj.BlueprintBlock.GetHashCode();
            }
        }
    }
}