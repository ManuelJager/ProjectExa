using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Utils;
using UnityEngine;

namespace Exa.Weapons {
    public class Projectile : MonoBehaviour {
        [SerializeField] private Rigidbody2D rb;
        private Damage damage;
        private BlockContext damageMask;
        private float lifeTime;
        private float timeAlive;

        public void Update() {
            var deltaTime = Time.deltaTime;
            timeAlive += deltaTime;

            if (timeAlive > lifeTime) {
                Destroy(gameObject);
            }
        }

        public void OnTriggerEnter2D(Collider2D collider) {
            var damageable = collider.gameObject.GetComponent<IDamageable>();

            if (!PassesDamageMask(damageable.Block)) {
                return;
            }

            var damageInstanceData = damageable.AbsorbDamage(damage);
            damage.value -= damageInstanceData.absorbedDamage;

            if (damage.value <= 0f) {
                Destroy(gameObject);
            }
        }

        public void Setup(Transform transform, float speed, float range, Damage damage, BlockContext damageMask) {
            this.transform.position = transform.position;
            this.damage = damage;
            lifeTime = range / speed;
            this.damageMask = damageMask;

            rb.velocity = transform.right * speed;
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