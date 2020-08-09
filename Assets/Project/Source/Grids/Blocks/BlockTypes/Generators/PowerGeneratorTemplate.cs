using Exa.Grids.Blocks.Components;
using Exa.UI.Tooltips;
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

        public PowerGeneratorTemplatePartial PowerGeneratorTemplatePartial { get => powerGeneratorTemplatePartial; set => powerGeneratorTemplatePartial = value; }

        public override void SetValues(PowerGenerator block)
        {
            base.SetValues(block);
            block.PowerGeneratorBehaviour.data = powerGeneratorTemplatePartial.Convert();
        }

        protected override PowerGenerator BuildOnGameObject(GameObject gameObject)
        {
            var instance = base.BuildOnGameObject(gameObject);
            instance.PowerGeneratorBehaviour = AddBlockBehaviour<PowerGeneratorBehaviour>(instance);
            return instance;
        }

        protected override IEnumerable<ITooltipComponent> TooltipComponentFactory()
        {
            return base.TooltipComponentFactory()
                .Concat(powerGeneratorTemplatePartial.GetComponents());
        }
    }
}