using System;
using Exa.Generics;
using Exa.Grids.Blueprints;
using Exa.Ships;
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

        protected void SpawnMothership(Blueprint mothership) {
            var ship = GameSystems.ShipFactory.CreateFriendly(mothership, Vector2.zero);
            var selection = ship.GetAppropriateSelection(new VicFormation());
            selection.Add(ship);
            GameSystems.GameplayInputManager.CurrentSelection = selection;
        }

        protected EnemyShip SpawnEnemy(string name, float xPos, float yPos) {
            var blueprint = Systems.Blueprints.GetBlueprint(name);
            var pos = new Vector2(xPos, yPos);
            return GameSystems.ShipFactory.CreateEnemy(blueprint, pos);
        }

        protected FriendlyShip SpawnFriendly(string name, float xPos, float yPos) {
            var blueprint = Systems.Blueprints.GetBlueprint(name);
            var pos = new Vector2(xPos, yPos);
            return GameSystems.ShipFactory.CreateFriendly(blueprint, pos);
        }
    }
}