using Exa.Grids.Blocks.Components;
using Exa.UI.Tooltips;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    [Serializable]
    [CreateAssetMenu(menuName = "Grids/Blocks/Thruster")]
    public class ThrusterTemplate : PhysicalBlockTemplate<Thruster>
    {
        [SerializeField] private ThrusterTemplatePartial thrusterTemplatePartial;

        public ThrusterTemplatePartial ThrusterTemplatePartial { get => thrusterTemplatePartial; set => thrusterTemplatePartial = value; }

        public override void SetValues(Thruster block)
        {
            base.SetValues(block);
            block.ThrusterBehaviour.data = thrusterTemplatePartial.Convert();
        }

        protected override Thruster BuildOnGameObject(GameObject gameObject)
        {
            var instance = base.BuildOnGameObject(gameObject);
            instance.ThrusterBehaviour = AddBlockBehaviour<ThrusterBehaviour>(instance);
            return instance;
        }

        protected override IEnumerable<ITooltipComponent> TooltipComponentFactory()
        {
            return base.TooltipComponentFactory()
                .Concat(thrusterTemplatePartial.GetComponents());
        }
    }
}