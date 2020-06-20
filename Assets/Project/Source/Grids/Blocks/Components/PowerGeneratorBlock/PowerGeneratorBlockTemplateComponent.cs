using Exa.Generics;
using Exa.UI.Controls;
using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class PowerGeneratorBlockTemplateComponent : ITemplateComponent<PowerGeneratorBlockData>, ITooltipPresenter
    {
        [SerializeField] private float maxGeneration; // In MW

        public PowerGeneratorBlockData Convert()
        {
            return new PowerGeneratorBlockData
            {
            };
        }

        public ITooltipComponent[] GetComponents()
        {
            return new ITooltipComponent[]
            {
                new ValueContext { name = "Power generation", value = $"{maxGeneration} KW"}
            };
        }
    }
}