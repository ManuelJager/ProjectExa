using Exa.Gameplay;
using Exa.Grids;
using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.Ships.Navigation;
using UnityEngine;

namespace Exa.Ships
{
    public class EnemyGridInstance : GridInstance
    {
        public INavigation Navigation { get; private set; }

        [SerializeField] private NavigationOptions navigationOptions;

        public override void Import(Blueprint blueprint, BlockContext blockContext, GridInstanceConfiguration configuration) {
            Navigation = navigationOptions.GetNavigation(this, blueprint);
            base.Import(blueprint, blockContext, configuration);
        }

        public float GetTurningRate() {
            return BlockGrid.Totals.TurningPower / BlockGrid.Totals.Mass;
        }

        public override ShipSelection GetAppropriateSelection(Formation formation) {
            return new EnemyShipSelection(formation);
        }

        public override bool MatchesSelection(ShipSelection selection) {
            return selection is EnemyShipSelection;
        }
    }
}