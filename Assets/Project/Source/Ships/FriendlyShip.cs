﻿using Exa.Gameplay;

namespace Exa.Ships
{
    public class FriendlyShip : Ship
    {
        public override ShipSelection GetAppropriateSelection(Formation formation) {
            return new FriendlyShipSelection(formation);
        }

        public override bool MatchesSelection(ShipSelection selection) {
            return selection is FriendlyShipSelection;
        }
    }
}