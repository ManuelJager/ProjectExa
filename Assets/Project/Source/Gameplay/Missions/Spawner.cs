using System.Collections.Generic;
using System.Linq;
using Exa.Grids;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blueprints;
using Exa.Math;
using Exa.Ships;
using Exa.Utils;
using UnityEngine;

namespace Exa.Gameplay.Missions
{
    public class Spawner
    {
        private IEnumerable<Blueprint> availableBlueprints;
        private PlayerStation station;

        public Spawner(IEnumerable<Blueprint> availableBlueprints) {
            this.availableBlueprints = availableBlueprints;
        }
        
        public IEnumerable<EnemyGrid> SpawnFormationAtRandomPosition(Formation formation, float distance) {
            var position = MathUtils.RandomFromAngledMagnitude(distance);
            var angle = (station.GetPosition() - position).GetAngle();
            return SpawnEnemyFormation(formation, position, angle, 100f);
        }

        public IEnumerable<EnemyGrid> SpawnEnemyFormation(Formation formation, Vector2 origin, float angle, float weight) {
            var blueprints = Enumerable.Repeat(availableBlueprints.GetRandomElement(), 5);
            var formationLayout = formation.GetGlobalLayout(blueprints, origin, angle);
            foreach (var (position, blueprint) in formationLayout.AsTupleEnumerable(blueprints)) {
                var enemy = GameSystems.ShipFactory.CreateEnemy(blueprint, position);
                enemy.SetRotation(angle);
                yield return enemy;
            }
        }

        public void SpawnRandomEnemy(float distance) {
            var blueprint = availableBlueprints.GetRandomElement();
            var position = MathUtils.RandomVector2(distance);
            var enemy = GameSystems.ShipFactory.CreateEnemy(blueprint, position);
            enemy.SetRotation(station.GetPosition());
        }
        
        public void SpawnPlayerStation(GridInstanceConfiguration? configuration = null) {
            var blueprint = Systems.Blueprints.GetBlueprint("defaultPlayerMothership");
            station = GameSystems.ShipFactory.CreateStation(blueprint, new Vector2(0, 0), configuration);
            ((ITurretPlatform)station.Controller).SetTarget(new MouseCursorTarget());
        }

        public EnemyGrid SpawnEnemy(string name, float xPos, float yPos, GridInstanceConfiguration? configuration = null) {
            var blueprint = Systems.Blueprints.GetBlueprint(name);
            var pos = new Vector2(xPos, yPos);
            return GameSystems.ShipFactory.CreateEnemy(blueprint, pos, configuration);
        }
    }
}