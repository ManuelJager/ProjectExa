using Exa.Weapons;
using System;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Grids.Blocks.Components
{
    public class AutocannonBehaviour : AutoFireTurret<AutocannonData>
    {
        [SerializeField] private AutocannonPart[] parts;
        private int currentPoint = 0;

        public override void Fire() {
            switch (data.cycleMode) {
                case CycleMode.Cycling:
                    parts[currentPoint].Fire(data.damage);
                    currentPoint++;
                    currentPoint %= parts.Length;
                    break;

                case CycleMode.Volley:
                    foreach (var part in parts)
                        part.Fire(data.damage);

                    break;
            }
        }

        protected override void OnAdd() {
            var animTime = GetAnimTime();
            if (Parent.BlockContext == 0) {
                throw new InvalidOperationException("Parent's block context must be set");
            }

            var damageMask = ~Parent.BlockContext;

            foreach (var part in parts)
                part.Setup(animTime, Parent, damageMask);
        }

        private float GetAnimTime() {
            switch (data.cycleMode) {
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