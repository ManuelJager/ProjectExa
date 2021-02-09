using System.Collections.Generic;
using Exa.Grids;
using Exa.Grids.Blueprints;
using Exa.Math;
using Exa.Utils;
using UnityEngine;

namespace Exa.Gameplay.Missions
{
    [CreateAssetMenu(menuName = "Missions/First")]
    public class FirstMission : Mission
    {
        [SerializeField] private List<StaticBlueprint> enemyBlueprints;
        
        public override void Init(MissionArgs args) {
            SpawnPlayerStation();
            SpawnRandomEnemy(100f);
        }

        private void SpawnRandomEnemy(float distance) {
            var blueprint = enemyBlueprints.GetRandomElement().GetBlueprint();
            var position = MathUtils.RandomVector2(distance);
            var enemy = GameSystems.ShipFactory.CreateEnemy(blueprint, position);
            Physics2D.SyncTransforms();
            enemy.SetRotation(Station.GetPosition());
        }
    }
}