using Exa.Grids.Blocks.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    [Serializable]
    [CreateAssetMenu(menuName = "Grids/Blocks/Turret")]
    public class TurretTemplate : BlockTemplate<Turret>
    {
        [SerializeField] private TurretTemplatePartial turretTemplatePartial;

        public override IEnumerable<TemplatePartialBase> GetTemplatePartials()
        {
            return base.GetTemplatePartials()
                .Append(turretTemplatePartial);
        }
    }
}