using Exa.Grids;
using Exa.Grids.Blocks;
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
        private object damageSource;

        public void Setup(object damageSource, BlockContext damageMask) {
            this.damageMask = damageMask;
            this.damageSource = damageSource;
        }

        public void Fire(float damage) {
            // TODO: Pool projectiles
            projectilePrefab.InstantiateAndGet<Projectile>(GameSystems.SpawnLayer.projectiles)
                .Setup(spawnPoint, 80f, 250f, damage, damageSource, damageMask);
        }
    }
}