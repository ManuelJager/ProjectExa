using Exa.Grids.Blocks;
using UnityEngine;

namespace Exa.Weapons
{
    public class FiringPoint : MonoBehaviour
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform spawnPoint;

        private ShipContext damageMask;

        public void Setup(ShipContext damageMask)
        {
            this.damageMask = damageMask;
        }

        public void Fire(float damage)  
        {
            // TODO: Pool projectiles
            var projectile = Instantiate(projectilePrefab).GetComponent<Projectile>();
            projectile.Setup(spawnPoint, 80f, 250f, damage, damageMask);
        }
    }
}