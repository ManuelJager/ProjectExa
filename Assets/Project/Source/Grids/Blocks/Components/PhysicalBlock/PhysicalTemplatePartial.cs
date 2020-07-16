using Exa.Generics;
using Exa.Grids.Blueprints;
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
        [SerializeField] private short mass; // In kg

        public PhysicalData Convert() => new PhysicalData
        {
            armor = armor,
            hull = maxHull,
            mass = mass
        };

        public virtual void AddContext(Blueprint blueprint)
        {
            blueprint.Mass += mass;
        }

        public virtual void RemoveContext(Blueprint blueprint)
        {
            blueprint.Mass -= mass;
        }

        public ITooltipComponent[] GetTooltipComponents() => new ITooltipComponent[]
        {
            new TooltipSpacer(),
            new NamedWrapper<string>("Hull", maxHull.ToString()),
            new NamedWrapper<string>("Armor", armor.ToString()),
            new NamedWrapper<string>("Mass", mass.ToString())
        };
    }
}