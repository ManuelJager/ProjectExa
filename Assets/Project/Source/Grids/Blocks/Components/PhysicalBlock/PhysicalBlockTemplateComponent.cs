using System;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class PhysicalBlockTemplateComponent : ITemplateComponent<PhysicalBlockData>
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
    }
}