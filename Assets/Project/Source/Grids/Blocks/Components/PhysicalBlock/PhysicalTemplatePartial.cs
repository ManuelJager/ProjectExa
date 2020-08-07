using Exa.Generics;
using Exa.Grids.Blueprints;
using Exa.UI.Tooltips;
using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    public interface IPhysicalTemplatePartial
    {
        PhysicalTemplatePartial PhysicalTemplatePartial { get; }
    }

    [Serializable]
    public class PhysicalTemplatePartial : TemplatePartial<PhysicalData>, IBlueprintTotalsModifier
    {
        [SerializeField] private float maxHull;
        [SerializeField] private float armor;
        [SerializeField] private short mass; // In kg


        public float MaxHull => maxHull;
        public float Armor => armor;
        public float Mass => mass;

        public override PhysicalData Convert() => new PhysicalData
        {
            armor = armor,
            hull = maxHull,
            mass = mass
        };

        public virtual void AddBlueprintTotals(Blueprint blueprint)
        {
            blueprint.Mass += mass;
        }

        public virtual void RemoveBlueprintTotals(Blueprint blueprint)
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