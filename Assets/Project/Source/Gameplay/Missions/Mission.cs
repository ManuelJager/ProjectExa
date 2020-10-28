using Exa.Generics;
using Exa.Grids.Blueprints;
using UnityEngine;

namespace Exa.Gameplay.Missions
{
    public abstract class Mission : ScriptableObject, ILabeledValue<Mission>
    {
        public string missionName;
        public string missionDescription;

        public string Label => missionName;
        public Mission Value => this;

        public abstract void Init(MissionArgs args);

        protected void SpawnMothership(BlueprintContainer mothership) {
            var ship = GameSystems.ShipFactory.CreateFriendly(mothership.Data, new Vector2(-20, 20));
            var cameraTarget = new ShipTarget(ship);
            GameSystems.CameraController.SetTarget(cameraTarget);
        }
    }
}