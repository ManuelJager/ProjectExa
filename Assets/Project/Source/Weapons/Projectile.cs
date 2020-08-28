using Exa.Math;
using UnityEngine;

namespace Exa.Weapons
{
    public class Projectile : MonoBehaviour
    {
        private Vector2 direction;
        private float damage;
        private float lifeTime;
        private float timeAlive;

        public void Setup(Vector2 position, Vector2 direction, float damage, float lifeTime)
        {
            transform.position = position;

            this.direction = direction;
            this.damage = damage;
            this.lifeTime = lifeTime;
        }

        public void Update()
        {
            var deltaTime = Time.deltaTime;

            transform.position += (direction * deltaTime).ToVector3();
            timeAlive += deltaTime;

            if (timeAlive > lifeTime)
            {
                Destroy(this);
            }
        }
    }
}