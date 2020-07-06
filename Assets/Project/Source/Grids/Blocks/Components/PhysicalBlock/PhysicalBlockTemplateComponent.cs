using Exa.Generics;
using Exa.UI.Controls;
using Exa.UI.Tooltips;
using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class PhysicalBlockTemplateComponent : ITemplateComponent<PhysicalBlockData>
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
                new TooltipSpacer(),
                new NamedValue<string>("Hull", maxHull.ToString()),
                new NamedValue<string>("Armor", armor.ToString()),
                new NamedValue<string>("Mass", mass.ToString())
            };
        }
    }
}