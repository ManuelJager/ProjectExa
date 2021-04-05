using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Utils;
using UnityEngine;

namespace Exa.Weapons
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        private float damage;
        private float lifeTime;
        private BlockContext damageMask;
        private float timeAlive;
        private object damageSource;

        public void Setup(Transform transform, float speed, float range, float damage, object damageSource, BlockContext damageMask) {
            this.transform.position = transform.position;
            this.damage = damage;
            this.lifeTime = range / speed;
            this.damageSource = damageSource;
            this.damageMask = damageMask;

            rb.velocity = transform.right * speed;
        }

        public void Update() {
            var deltaTime = Time.deltaTime;
            timeAlive += deltaTime;

            if (timeAlive > lifeTime) {
                Destroy(gameObject);
            }
        }

        public void OnTriggerEnter2D(Collider2D collider) {
            var block = collider.gameObject.GetComponent<Block>();
            if (!block) {
                Debug.LogError($"Collided with {collider.gameObject}, but found no block attached");
                return;
            }
            if (!PassesDamageMask(block)) return;

            var damageInstanceData = block.PhysicalBehaviour.AbsorbDamage(damageSource, damage);
            damage -= damageInstanceData.absorbedDamage;

            if (damage <= 0f) {
                Destroy(gameObject);
            }
        }

        private bool PassesDamageMask(Block block) {
            if (block.Parent == null) {
                Debug.LogError($"Block {block.GetInstanceID()} has no parent");
                return false;
            }

            return block.Parent.BlockContext.HasAnyValue(damageMask);
        }
    }
}