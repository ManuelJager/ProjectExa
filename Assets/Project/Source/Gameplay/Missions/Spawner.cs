﻿using System.Collections.Generic;
using Exa.Grids;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using Exa.Grids.Blueprints;
using Exa.Math;
using Exa.Ships;
using Exa.Utils;
using UnityEngine;

namespace Exa.Gameplay.Missions
{
    public class Spawner
    {
        private PlayerStation station;

        public IEnumerable<EnemyGrid> SpawnFormationAtRandomPosition(IWave wave, Formation formation, float distance) {
            var position = MathUtils.RandomFromAngledMagnitude(distance);
            var angle = (station.GetPosition() - position).GetAngle();
            return SpawnEnemyFormation(wave, formation, position, angle, 100f);
        }

        public IEnumerable<EnemyGrid> SpawnEnemyFormation(IWave wave, Formation formation, Vector2 origin, float angle, float weight) {
            var blueprints = wave.GetSpawnAbleBlueprints();
            var formationLayout = formation.GetGlobalLayout(blueprints, origin, angle);
            foreach (var (position, blueprint) in formationLayout.AsTupleEnumerable(blueprints)) {
                var enemy = GameSystems.ShipFactory.CreateEnemy(blueprint, position);
                enemy.SetRotation(angle);
                yield return enemy;
            }
        }

        public void SpawnRandomEnemy(IWave wave, float distance) {
            var blueprint = wave.GetSpawnAbleBlueprints().GetRandomElement();
            var position = MathUtils.RandomVector2(distance);
            var enemy = GameSystems.ShipFactory.CreateEnemy(blueprint, position);
            enemy.SetLookAt(station.GetPosition());
        }
        
        public PlayerStation SpawnPlayerStation(Blueprint blueprint = null, GridInstanceConfiguration? configuration = null) {
            blueprint ??= Systems.Blueprints.GetBlueprint("defaultPlayerMothership");
            station = GameSystems.ShipFactory.CreateStation(blueprint, new Vector2(0, 0), configuration);
            station.Controller.GetBehaviour<ITurretBehaviour>().Target = new MouseCursorTarget();
            return station;
        }

        public EnemyGrid SpawnEnemy(string name, float xPos, float yPos, GridInstanceConfiguration? configuration = null) {
            var blueprint = Systems.Blueprints.GetBlueprint(name);
            var pos = new Vector2(xPos, yPos);
            return GameSystems.ShipFactory.CreateEnemy(blueprint, pos, configuration);
        }
    }
}