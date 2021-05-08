using Exa.Gameplay;
using Exa.Grids;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using Exa.Grids.Blueprints;
using Exa.Input;
using Project.Source.Grids;
using UnityEngine;
using UnityEngine.InputSystem;
using static Exa.Input.GameControls;

namespace Exa.Ships
{
    public class PlayerStation : GridInstance, IPlayerStationActions
    {
        private GameControls gameControls;

        public BlockGridDiff Diff { get; private set; }

        protected override void Awake() {
            base.Awake();
            
            gameControls = new GameControls();
            gameControls.PlayerStation.SetCallbacks(this);
        }
        
        public void OnEnable() {
            gameControls.Enable();
        }

        public void OnDisable() {
            gameControls.Disable();
        }
        
        public override void Import(Blueprint blueprint, BlockContext blockContext, GridInstanceConfiguration configuration) {
            base.Import(blueprint, blockContext, configuration);
            Diff = Systems.Blocks.Diffs.StartWatching(BlockGrid, Blueprint.Grid);
            Overlay = GameSystems.UI.gameplayLayer.coreHealthBar;
        }

        public override void SetBlueprint(Blueprint blueprint) {
            base.SetBlueprint(blueprint);
            Diff.TrackNewTarget(blueprint.Grid);
        }

        public override void OnControllerDestroyed() {
            base.OnControllerDestroyed();
            gameControls.Disable();
            gameControls = null;
        }

        public override ShipSelection GetAppropriateSelection(Formation formation) {
            return new FriendlyShipSelection(formation);
        }

        public override bool MatchesSelection(ShipSelection selection) {
            return selection is FriendlyShipSelection;
        }

        public override Vector2 GetPosition() {
            return transform.position + Controller.transform.localPosition;
        }

        public override void SetPosition(Vector2 position) {
            transform.position = position - (Vector2)Controller.transform.localPosition;
        }

        public void OnFire(InputAction.CallbackContext context) {
            if (Controller is IChargeableTurretPlatform chargeableTurret) {
                switch (context.phase) {
                    case InputActionPhase.Started:
                        chargeableTurret.StartCharge();
                        break;
                    case InputActionPhase.Canceled:
                        chargeableTurret.EndChange();
                        break;
                }

                return;
            }

            if (Controller is ITurretPlatform turret) {
                switch (context.phase) {
                    case InputActionPhase.Started:
                        turret.Fire();
                        break;
                }
            }
        }
    }
}