using Exa.Grids.Blocks.Components;
using Exa.UI.Tooltips;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    [Serializable]
    [CreateAssetMenu(menuName = "Grids/Blocks/Gyroscope")]
    public class GyroscopeTemplate : PhysicalBlockTemplate<Gyroscope>, IGyroscopeTemplatePartial
    {
        [SerializeField] private GyroscopeTemplatePartial gyroscopeTemplatePartial;

        public GyroscopeTemplatePartial GyroscopeTemplatePartial { get => gyroscopeTemplatePartial; set => gyroscopeTemplatePartial = value; }

        public override void SetValues(Gyroscope block)
        {
            base.SetValues(block);
            block.GyroscopeBehaviour.data = gyroscopeTemplatePartial.Convert();
        }

        protected override Gyroscope BuildOnGameObject(GameObject gameObject)
        {
            var instance = base.BuildOnGameObject(gameObject);
            instance.GyroscopeBehaviour = AddBlockBehaviour<GyroscopeBehaviour>(instance);
            return instance;
        }

        protected override IEnumerable<ITooltipComponent> TooltipComponentFactory()
        {
            return base.TooltipComponentFactory()
                .Concat(gyroscopeTemplatePartial.GetComponents());
        }
    }
}