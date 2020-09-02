using Exa.Gameplay;
using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.UI;
using UnityEngine;

namespace Exa.Ships
{
    public class FriendlyShip : Ship
    {
        public override ShipSelection GetAppropriateSelection(Formation formation)
        {
            return new FriendlyShipSelection(formation);
        }

        public override bool MatchesSelection(ShipSelection selection)
        {
            return selection is FriendlyShipSelection;
        }
    }
}