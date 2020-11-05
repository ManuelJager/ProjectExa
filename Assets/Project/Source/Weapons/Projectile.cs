using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using Exa.Math;
using UnityEngine;

namespace Exa.Weapons
{
    public class Projectile : MonoBehaviour
    {
        private Vector2 direction;
        private float damage;
        private float lifeTime;
        private ShipContext damageMask;
        private float timeAlive;

        public void Setup(Transform transform, float speed, float range, float damage, ShipContext damageMask) {
            this.transform.position = transform.position;
            this.damage = damage;
            this.direction = transform.right * speed;
            this.lifeTime = range / speed;
            this.damageMask = damageMask;
        }

        public void Update() {
            var deltaTime = Time.deltaTime;

            transform.position += (direction * deltaTime).ToVector3();
            timeAlive += deltaTime;

            if (timeAlive > lifeTime) {
                Destroy(gameObject);
            }
        }

        public void OnTriggerEnter2D(Collider2D collider) {
            var block = collider.transform.GetComponent<Block>();
            if (!block) return;

            if ((block.Ship.BlockContext & damageMask) != 0) {
                block.PhysicalBehaviour.TakeDamage(damage);
            }
        }
    }
}