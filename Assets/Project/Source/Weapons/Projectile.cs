using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using Exa.Math;
using UnityEngine;

namespace Exa.Weapons
{
    public class Projectile : MonoBehaviour
    {
        private Vector2 _direction;
        private float _damage;
        private float _lifeTime;
        private ShipContext _damageMask;
        private float _timeAlive;

        public void Setup(Transform transform, float speed, float range, float damage, ShipContext damageMask)
        {
            this.transform.position = transform.position;
            this._damage = damage;
            this._direction = transform.right * speed;
            this._lifeTime = range / speed;
            this._damageMask = damageMask;
        }

        public void Update()
        {
            var deltaTime = Time.deltaTime;

            transform.position += (_direction * deltaTime).ToVector3();
            _timeAlive += deltaTime;

            if (_timeAlive > _lifeTime)
            {
                Destroy(gameObject);
            }
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            var block = collision.transform.GetComponent<Block>();
            if (!block) return;
            
            if ((block.Ship.BlockContext & _damageMask) != 0)
            {
                var physicalBehaviour = (block as IBehaviourMarker<PhysicalData>).Component as PhysicalBehaviour;
                physicalBehaviour.TakeDamage(_damage);
            }
        }
    }
}