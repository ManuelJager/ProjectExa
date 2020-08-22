using Exa.Generics;
using Exa.UI.Tooltips;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class ControllerTemplatePartial : TemplatePartial<ControllerData>
    {
        [SerializeField] private float powerGeneration;
        [SerializeField] private float powerConsumption;
        [SerializeField] private float powerStorage;
        [SerializeField] private float turningRate;

        public override ControllerData Convert() => new ControllerData
        {
            powerGeneration = powerGeneration,
            powerConsumption = powerConsumption,
            powerStorage = powerStorage,
            turningRate = turningRate
        };

        public override IEnumerable<ITooltipComponent> GetTooltipComponents() => new ITooltipComponent[]
        {
            new TooltipSpacer(),
            new LabeledValue<string>("Power generation", $"{powerGeneration} KW"),
            new LabeledValue<string>("Power consumption", $"{powerConsumption} KW"),
            new LabeledValue<string>("Power storage", $"{powerStorage} KJ"),
            new LabeledValue<string>("Turning rate", $"{turningRate}")
        };
    }
}