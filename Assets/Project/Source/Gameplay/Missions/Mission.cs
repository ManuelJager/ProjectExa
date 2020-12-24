using System;
using System.Collections;
using Exa.Generics;
using Exa.Grids;
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

        protected void SpawnPlayerStation(GridInstanceConfiguration configuration = default) {
            var blueprint = Systems.Blueprints.GetBlueprint("defaultPlayerMothership");
            var station = GameSystems.ShipFactory.CreateStation(blueprint, new Vector2(0, 0), configuration);
            GameSystems.UI.gameplayLayer.coreHealthBar.TrackHealth(station);
        }

        protected EnemyGridInstance SpawnEnemy(string name, float xPos, float yPos, GridInstanceConfiguration configuration = default) {
            var blueprint = Systems.Blueprints.GetBlueprint(name);
            var pos = new Vector2(xPos, yPos);
            return GameSystems.ShipFactory.CreateEnemy(blueprint, pos, configuration);
        }

        protected Coroutine StartCoroutine(IEnumerator enumerator) {
            return GameSystems.Instance.StartCoroutine(enumerator);
        }
    }
}