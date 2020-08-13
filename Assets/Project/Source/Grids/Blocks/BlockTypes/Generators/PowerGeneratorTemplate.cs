using Exa.Grids.Blocks.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    [Serializable]
    [CreateAssetMenu(menuName = "Grids/Blocks/PowerGenerator")]
    public class PowerGeneratorTemplate : BlockTemplate<PowerGenerator>, IPowerGeneratorTemplatePartial
    {
        [SerializeField] private PowerGeneratorTemplatePartial powerGeneratorTemplatePartial;

        public PowerGeneratorTemplatePartial PowerGeneratorTemplatePartial 
        {
            get => powerGeneratorTemplatePartial; 
            set => powerGeneratorTemplatePartial = value; 
        }

        protected override PowerGenerator BuildOnGameObject(GameObject gameObject)
        {
            var instance = base.BuildOnGameObject(gameObject);
            instance.PowerGeneratorBehaviour = AddBlockBehaviour<PowerGeneratorBehaviour>(instance);
            return instance;
        }

        protected override IEnumerable<TemplatePartialBase> GetTemplatePartials()
        {
            return base.GetTemplatePartials()
                .Append(powerGeneratorTemplatePartial);
        }
    }
}