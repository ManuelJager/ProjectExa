using UnityEngine;

namespace Exa.Weapons
{
    public class FiringPoint : MonoBehaviour
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform spawnPoint;

        public void Fire(float damage)
        {
            // TODO: Pool projectiles
            var projectile = Instantiate(projectilePrefab).GetComponent<Projectile>();
            projectile.Setup(spawnPoint.position, spawnPoint.right * 80, damage, 10f);
        }
    }
}