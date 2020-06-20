using Exa.Generics;
using Exa.UI.Controls;
using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class PhysicalBlockTemplateComponent : ITemplateComponent<PhysicalBlockData>, ITooltipPresenter
    {
        [SerializeField] private float maxHull;
        [SerializeField] private float armor;
        [SerializeField] private long mass; // In kg

        public float MaxHull => maxHull;
        public float Armor => armor;
        public long Mass => mass;

        public PhysicalBlockData Convert()
        {
            return new PhysicalBlockData
            {
                armor = armor,
                hull = maxHull
            };
        }

        public ITooltipComponent[] GetComponents()
        {
            return new ITooltipComponent[]
            {
                new ValueContext { name = "Hull", value = maxHull.ToString() },
                new ValueContext { name = "Armor", value = armor.ToString() },
                new ValueContext { name = "Mass", value = mass.ToString()}
            };
        }
    }
}