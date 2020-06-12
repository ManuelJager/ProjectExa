using Exa.Generics;
using Exa.UI.Controls;
using System;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class PhysicalBlockTemplateComponent : ITemplateComponent<PhysicalBlockData>, ITooltipPresenter
    {
        public float maxHealth;
        public float armor;

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
                new ValueContext { name = "Armor", value = armor.ToString() }
            };
        }
    }
}