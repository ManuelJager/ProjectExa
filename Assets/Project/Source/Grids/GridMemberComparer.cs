using System.Collections.Generic;

namespace Exa.Grids {
    public class GridMemberComparer : IEqualityComparer<IGridMember> {
        private static GridMemberComparer defaultComparer;

        public static GridMemberComparer Default {
            get => defaultComparer ??= new GridMemberComparer();
        }

        public bool Equals(IGridMember x, IGridMember y) {
            if (ReferenceEquals(x, y)) {
                return true;
            }

            if (ReferenceEquals(x, null)) {
                return false;
            }

            if (ReferenceEquals(y, null)) {
                return false;
            }

            return x.GridAnchor.Equals(y.GridAnchor) && x.BlueprintBlock.Equals(y.BlueprintBlock);
        }

        public int GetHashCode(IGridMember obj) {
            unchecked {
                return (obj.GridAnchor.GetHashCode() * 397) ^ obj.BlueprintBlock.GetHashCode();
            }
        }
    }
}