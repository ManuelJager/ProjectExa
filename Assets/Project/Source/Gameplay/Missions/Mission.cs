using System;
using System.Collections;
using Exa.Grids;
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

        public abstract void Init(MissionArgs args);

        protected PlayerStation SpawnPlayerStation(GridInstanceConfiguration configuration = default) {
            var blueprint = Systems.Blueprints.GetBlueprint("defaultPlayerMothership"); 
            return GameSystems.ShipFactory.CreateStation(blueprint, new Vector2(0, 0), configuration);
        }

        protected EnemyGrid SpawnEnemy(string name, float xPos, float yPos, GridInstanceConfiguration configuration = default) {
            var blueprint = Systems.Blueprints.GetBlueprint(name);
            var pos = new Vector2(xPos, yPos);
            return GameSystems.ShipFactory.CreateEnemy(blueprint, pos, configuration);
        }

        protected Coroutine StartCoroutine(IEnumerator enumerator) {
            return GameSystems.Instance.StartCoroutine(enumerator);
        }
    }
}