using Exa.Grids.Blocks.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    [Serializable]
    [CreateAssetMenu(menuName = "Grids/Blocks/PowerGenerator")]
    public class PowerGeneratorTemplate : BlockTemplate<PowerGenerator>
    {
        [SerializeField] private PowerGeneratorTemplatePartial powerGeneratorTemplatePartial;

        public PowerGeneratorTemplatePartial PowerGeneratorTemplatePartial 
        {
            get => powerGeneratorTemplatePartial; 
            set => powerGeneratorTemplatePartial = value; 
        }

        protected override IEnumerable<TemplatePartialBase> GetTemplatePartials()
        {
            return base.GetTemplatePartials()
                .Append(powerGeneratorTemplatePartial);
        }
    }
}