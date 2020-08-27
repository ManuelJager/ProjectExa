using UnityEngine;

namespace Exa.Weapons
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        private float? damage;

        public void Setup(Vector2 position, Vector2 force, float damage)
        {
            transform.position = position;
            rb.AddForce(force);

            this.damage = damage;
        }
    }
}