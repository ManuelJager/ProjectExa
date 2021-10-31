using System.Collections.Generic;
using System.Linq;
using Exa.Grids;

namespace Exa.Gameplay {
    public class OrchestratorGridClaims : List<OrchestratorGridClaim> {
        public IEnumerable<T> Except<T>(IEnumerable<T> input)
            where T : IGridMember {
            bool Selector(T item) {
                return this.All(claim => claim.gridAnchor != item.GridAnchor);
            }

            return input.Where(Selector);
        }
    }
}