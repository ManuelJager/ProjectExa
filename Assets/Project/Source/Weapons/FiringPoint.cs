using Exa.Grids.Blocks;
using UnityEngine;

namespace Exa.Weapons
{
    public class FiringPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private Transform _spawnPoint;

        private ShipContext _damageMask;

        public void Setup(ShipContext damageMask)
        {
            this._damageMask = damageMask;
        }

        public void Fire(float damage)  
        {
            // TODO: Pool projectiles
            var projectile = Instantiate(_projectilePrefab).GetComponent<Projectile>();
            projectile.Setup(_spawnPoint, 80f, 250f, damage, _damageMask);
        }
    }
}