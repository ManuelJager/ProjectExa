using Exa.Grids.Blocks.Components;
using UnityEngine;

namespace Exa.Weapons
{
    public class AutocannonBehaviour : TurretBehaviour
    {
        [SerializeField] private AutocannonPart[] parts;
        private int currentPoint = 0;

        private void Start()
        {
            foreach (var part in parts)
            {
                part.Setup(data.firingRate);
            }
        }

        public override void Fire()
        {
            parts[currentPoint].Fire(data.damage);
            currentPoint++;
            currentPoint %= parts.Length;
        }
    }
}