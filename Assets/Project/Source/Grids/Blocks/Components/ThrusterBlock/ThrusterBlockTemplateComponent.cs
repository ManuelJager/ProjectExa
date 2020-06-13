using Exa.Generics;
using Exa.UI.Controls;
using System;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class ThrusterBlockTemplateComponent : ITemplateComponent<ThrusterBlockData>, ITooltipPresenter
    {
        public float newtonThrust;

        public ThrusterBlockData Convert()
        {
            return new ThrusterBlockData
            {
                newtonThrust = newtonThrust
            };
        }

        public ITooltipComponent[] GetComponents()
        {
            return new ITooltipComponent[]
            {
                new ValueContext { name = "Thrust", value = $"{newtonThrust}N"}
            };
        }
    }
}