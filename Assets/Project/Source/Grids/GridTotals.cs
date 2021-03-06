﻿using Exa.Data;
using Exa.Generics;
using Exa.Grids.Blocks.Components;
using Exa.UI.Tooltips;
using System.Collections.Generic;

namespace Exa.Grids
{
    public class GridTotals : ICloneable<GridTotals>
    {
        public ControllerData controllerData;

        public virtual float Mass { get; set; }
        public virtual float Hull { get; set; }
        public virtual Scalar PowerGenerationModifier { get; set; }
        public virtual Scalar PowerConsumptionModifier { get; set; }
        public virtual Scalar PowerStorageModifier { get; set; }
        public virtual Scalar TurningPowerModifier { get; set; }

        public virtual float PowerGeneration => PowerGenerationModifier.GetValue(controllerData.powerGeneration);
        public virtual float PowerConsumption => PowerConsumptionModifier.GetValue(controllerData.powerConsumption);
        public virtual float PowerStorage => PowerStorageModifier.GetValue(controllerData.powerStorage);
        public virtual float TurningPower => TurningPowerModifier.GetValue(controllerData.turningRate);

        public GridTotals Clone() {
            return new GridTotals {
                controllerData = controllerData,
                Mass = Mass,
                Hull = Hull,
                PowerGenerationModifier = PowerGenerationModifier,
                PowerConsumptionModifier = PowerConsumptionModifier,
                PowerStorageModifier = PowerStorageModifier,
                TurningPowerModifier = TurningPowerModifier,
            };
        }

        public IEnumerable<ITooltipComponent> GetDebugTooltipComponents() => new ITooltipComponent[] {
            new TooltipText($"Mass: {Mass}"),
            new TooltipText($"Hull: {Hull}"),
        };
    }
}