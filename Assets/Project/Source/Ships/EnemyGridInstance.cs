using Exa.Gameplay;

namespace Exa.Ships
{
    public class EnemyGridInstance : GridInstance
    {
        public override ShipSelection GetAppropriateSelection(Formation formation) {
            return new EnemyShipSelection(formation);
        }

        public override bool MatchesSelection(ShipSelection selection) {
            return selection is EnemyShipSelection;
        }
    }
}