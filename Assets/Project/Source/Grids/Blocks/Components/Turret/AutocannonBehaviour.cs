using Exa.Weapons;
using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    public class AutocannonBehaviour : TurretBehaviour<AutocannonData>
    {
        [SerializeField] private AutocannonPart[] _parts;
        private int _currentPoint = 0;

        private void Start()
        {
            var animTime = GetAnimTime();
            var damageMask = ~Ship.BlockContext;

            foreach (var part in _parts)
            {
                part.Setup(animTime, damageMask);
            }
        }

        public override void Fire()
        {
            switch (data.cycleMode)
            {
                case CycleMode.Cycling:
                    _parts[_currentPoint].Fire(data.damage);
                    _currentPoint++;
                    _currentPoint %= _parts.Length;
                    break;

                case CycleMode.Volley:
                    for (int i = 0; i < _parts.Length; i++)
                    {
                        _parts[i].Fire(data.damage);
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