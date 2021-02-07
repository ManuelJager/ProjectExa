using System;
using System.Collections;
using Exa.Grids;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Ships;
using Exa.Types.Generics;
using UnityEngine;

namespace Exa.Gameplay.Missions
{
    public abstract class Mission : ScriptableObject, ILabeledValue<Mission>
    {
        public string missionName;
        public string missionDescription;

        public string Label => missionName;
        public Mission Value => this;
        public PlayerStation Station { get; protected set; }

        public abstract void Init(MissionArgs args);

        public void Update() {
        }

        protected void SpawnPlayerStation(GridInstanceConfiguration? configuration = null) {
            var blueprint = Systems.Blueprints.GetBlueprint("defaultPlayerMothership");
            Station = GameSystems.ShipFactory.CreateStation(blueprint, new Vector2(0, 0), configuration);
            ((ITurretPlatform)Station.Controller).SetTarget(new MouseCursorTarget());
        }

        protected EnemyGrid SpawnEnemy(string name, float xPos, float yPos, GridInstanceConfiguration? configuration = null) {
            var blueprint = Systems.Blueprints.GetBlueprint(name);
            var pos = new Vector2(xPos, yPos);
            return GameSystems.ShipFactory.CreateEnemy(blueprint, pos, configuration);
        }
    }
}