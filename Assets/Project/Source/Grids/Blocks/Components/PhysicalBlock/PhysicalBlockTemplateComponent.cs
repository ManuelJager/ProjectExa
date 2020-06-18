using Exa.Generics;
using Exa.UI.Controls;
using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class PhysicalBlockTemplateComponent : ITemplateComponent<PhysicalBlockData>, ITooltipPresenter
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private float armor;
        [SerializeField] private long mass; // In kg

        public float MaxHealth => maxHealth;
        public float Armor => armor;
        public long Mass => mass;

        public PhysicalBlockData Convert()
        {
            return new PhysicalBlockData
            {
                armor = armor,
                health = maxHealth
            };
        }

        public ITooltipComponent[] GetComponents()
        {
            return new ITooltipComponent[]
            {
                new ValueContext { name = "Health", value = maxHealth.ToString() },
                new ValueContext { name = "Armor", value = armor.ToString() },
                new ValueContext { name = "Mass", value = mass.ToString()}
            };
        }
    }
}