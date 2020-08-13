using Exa.Generics;
using Exa.Grids.Blocks.BlockTypes;
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
    public class PhysicalTemplatePartial : TemplatePartial<PhysicalData>
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

        public override void SetValues(Block block)
        {
            (block as IPhysical).PhysicalBehaviour.SetData(Convert());
        }

        public override ITooltipComponent[] GetTooltipComponents() => new ITooltipComponent[]
        {
            new TooltipSpacer(),
            new NamedValue<string>("Hull", maxHull.ToString()),
            new NamedValue<string>("Armor", armor.ToString()),
            new NamedValue<string>("Mass", mass.ToString())
        };
    }
}