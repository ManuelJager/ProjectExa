using Exa.Weapons;
using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    public class AutocannonBehaviour : TurretBehaviour<AutocannonData>
    {
        [SerializeField] private AutocannonPart[] parts;
        private int currentPoint = 0;

        private void Start()
        {
            var animTime = GetAnimTime();
            foreach (var part in parts)
            {
                part.Setup(animTime);
            }
        }

        public override void Fire()
        {
            switch (data.cycleMode)
            {
                case CycleMode.Cycling:
                    parts[currentPoint].Fire(data.damage);
                    currentPoint++;
                    currentPoint %= parts.Length;
                    break;

                case CycleMode.Volley:
                    for (int i = 0; i < parts.Length; i++)
                    {
                        parts[i].Fire(data.damage);
                    }
                    break;
            }
        }

        private float GetAnimTime()
        {
            switch (data.cycleMode)
            {
                case CycleMode.Cycling:
                    return data.firingRate * 1.5f;

                case CycleMode.Volley:
                    return data.firingRate * 0.75f;

                default:
                    throw new Exception();
            }
        }
    }
}