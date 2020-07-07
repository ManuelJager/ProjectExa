using Exa.Generics;
using Exa.Grids.Blocks.BlockTypes;
using Exa.UI.Tooltips;
using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class GyroscopeBlockTemplateComponent : ITemplateComponent<GyroscopeBlockData>
    {
        [SerializeField] private float turningRate; // In tons it can rotate 360 degrees per second

        public float TurningRate => turningRate;

        public GyroscopeBlockData Convert()
        {
            return new GyroscopeBlockData
            {
                turningRate = turningRate
            };
        }

        public ITooltipComponent[] GetComponents()
        {
            return new ITooltipComponent[]
            {
                new TooltipSpacer(),
                new NamedWrapper<string>("Turning Rate", turningRate.ToString())
            };
        }
    }
}