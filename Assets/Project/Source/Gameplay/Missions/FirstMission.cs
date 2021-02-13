using System.Collections.Generic;
using System.Linq;
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
            SpawnFormationAtRandomPosition(new VicFormation(), 100f);
        }

        private void SpawnFormationAtRandomPosition(Formation formation, float distance) {
            var position = MathUtils.RandomVector2(distance);
            var angle = (Station.GetPosition() - position).GetAngle();
            SpawnEnemyFormation(formation, position, angle, 100f);
        }

        private void SpawnEnemyFormation(Formation formation, Vector2 origin, float angle, float weight) {
            var blueprints = Enumerable.Repeat(enemyBlueprints.GetRandomElement().GetBlueprint(), 5);
            var formationLayout = formation.GetGlobalLayout(blueprints, origin, angle);
            foreach (var (position, blueprint) in formationLayout.AsTupleEnumerable(blueprints)) {
                var enemy = GameSystems.ShipFactory.CreateEnemy(blueprint, position);
                enemy.SetRotation(angle);
            }
        }

        private void SpawnRandomEnemy(float distance) {
            var blueprint = enemyBlueprints.GetRandomElement().GetContainer();
            var position = MathUtils.RandomVector2(distance);
            var enemy = GameSystems.ShipFactory.CreateEnemy(blueprint.Data, position);
            enemy.SetRotation(Station.GetPosition());
        }
    }
}