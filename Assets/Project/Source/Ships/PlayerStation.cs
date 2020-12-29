using Exa.Gameplay;
using Exa.Grids;
using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using UnityEngine;

namespace Exa.Ships
{
    public class PlayerStation : GridInstance
    {
        public override void Import(Blueprint blueprint, BlockContext blockContext, GridInstanceConfiguration configuration) {
            base.Import(blueprint, blockContext, configuration);
            Overlay = GameSystems.UI.gameplayLayer.coreHealthBar;
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
    }
}