using Exa.Grids.Blocks.Components;
using UnityEngine;

namespace Exa.Weapons
{
    public class AutocannonBehaviour : TurretBehaviour
    {
        [SerializeField] private FiringPoint[] firingPoints;
        private int currentPoint = 0;

        public override void Fire()
        {
            firingPoints[currentPoint].Fire(data.damage);
            currentPoint++;
            currentPoint %= firingPoints.Length;
        }
    }
}