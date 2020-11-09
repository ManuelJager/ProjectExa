using UnityEngine;

namespace Exa.Utils
{
    public static class Collider2DExtensions
    {
        public static void SetMass(this BoxCollider2D collider, float mass) {
            var size = collider.size;
            var area = size.x * size.y;
            collider.density = area * mass;
        }
    }
}