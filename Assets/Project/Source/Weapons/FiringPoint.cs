﻿using Exa.Grids.Blocks;
using Exa.Utils;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Weapons
{
    public class FiringPoint : MonoBehaviour
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform spawnPoint;

        private BlockContext damageMask;

        public void Setup(BlockContext damageMask) {
            this.damageMask = damageMask;
        }

        public void Fire(float damage) {
            // TODO: Pool projectiles
            this.InstantiateAndGet<Projectile>(projectilePrefab, GameSystems.SpawnLayer.projectiles)
                .Setup(spawnPoint, 80f, 250f, damage, damageMask);
        }
    }
}