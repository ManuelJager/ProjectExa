using Exa.Debugging;
using Exa.Gameplay;
using Exa.Grids;
using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.Math;
using Exa.Ships.Navigation;
using UnityEngine;

namespace Exa.Ships
{
    public class EnemyGrid : GridInstance, IDebugDragable
    {
        public INavigation Navigation { get; private set; }

        [SerializeField] private NavigationOptions navigationOptions;

        public override void Import(Blueprint blueprint, BlockContext blockContext, GridInstanceConfiguration configuration) {
            Navigation = navigationOptions.GetNavigation(this, blueprint);
            base.Import(blueprint, blockContext, configuration);
            Overlay = GameSystems.ShipFactory.CreateOverlay(this);
        }

        protected override void ActiveFixedUpdate(float fixedDeltaTime) {
            Navigation?.Update(fixedDeltaTime);
            base.ActiveFixedUpdate(fixedDeltaTime);
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

        public override Vector2 GetPosition() {
            return (Vector2)transform.position + Rigidbody2D.centerOfMass.Rotate(-Rigidbody2D.rotation);
        }

        public override void SetPosition(Vector2 position) {
            transform.position = position - Rigidbody2D.centerOfMass.Rotate(-Rigidbody2D.rotation);
        }

        public Vector2 GetDebugDraggerPosition() {
            return transform.position;
        }

        public void SetDebugDraggerGlobals(Vector2 position, Vector2 velocity) {
            transform.position = position;
            Rigidbody2D.velocity = velocity;
        }
    }
}