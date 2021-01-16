using Exa.Grids.Blocks.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    [Serializable]
    [CreateAssetMenu(menuName = "Grids/Blocks/Autocannon")]
    public class AutocannonTemplate : BlockTemplate<Autocannon>, ITurretTemplate
    {
        [SerializeField] private float turretRadius;
        [SerializeField] private AutocannonTemplatePartial autocannonTemplatePartial;

        public float TurretRadius => turretRadius;

        public override IEnumerable<TemplatePartialBase> GetTemplatePartials() {
            return base.GetTemplatePartials()
                .Append(autocannonTemplatePartial);
        }
    }
}