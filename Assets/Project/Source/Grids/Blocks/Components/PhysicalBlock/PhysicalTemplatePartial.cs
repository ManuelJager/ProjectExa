using Exa.Generics;
using Exa.UI.Tooltips;
using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class PhysicalTemplatePartial : ITemplatePartial<PhysicalData>
    {
        [SerializeField] private float maxHull;
        [SerializeField] private float armor;
        [SerializeField] private long mass; // In kg

        public PhysicalData Convert() => new PhysicalData
        {
            armor = armor,
            hull = maxHull
        };

        public ITooltipComponent[] GetTooltipComponents() => new ITooltipComponent[]
        {
            new TooltipSpacer(),
            new NamedWrapper<string>("Hull", maxHull.ToString()),
            new NamedWrapper<string>("Armor", armor.ToString()),
            new NamedWrapper<string>("Mass", mass.ToString())
        };
    }
}