using Exa.Data;
using Exa.Generics;
using Exa.Grids.Blocks.Components;
using Exa.UI.Tooltips;
using Exa.Utils;
using System.Collections.Generic;
using System.Text;

namespace Exa.Grids
{
    public class GridTotals : ICloneable<GridTotals>
    {
        public ControllerData controllerData;

        private long mass;
        private float hull;
        private Percentage powerGenerationModifier = new Percentage(1);
        private Percentage peakPowerConsumptionModifier = new Percentage(1);
        private Percentage powerStorageModifier = new Percentage(1);
        private Percentage turningPowerModifier = new Percentage(1);

        public virtual long Mass
        {
            get => mass;
            set => mass = value;
        }

        public virtual float Hull
        {
            get => hull;
            set => hull = value;
        }

        public virtual Percentage PowerGenerationModifier
        {
            get => powerGenerationModifier;
            set => powerGenerationModifier = value;
        }

        public virtual Percentage PowerConsumptionModifier
        {
            get => peakPowerConsumptionModifier;
            set => peakPowerConsumptionModifier = value;
        }

        public virtual Percentage PowerStorageModifier
        {
            get => powerStorageModifier;
            set => powerStorageModifier = value;
        }

        public virtual Percentage TurningPowerModifier
        {
            get => turningPowerModifier;
            set => turningPowerModifier = value;
        }

        public virtual float PowerGeneration
        {
            get => PowerGenerationModifier.GetValue(controllerData.powerGeneration);
        }

        public virtual float PowerConsumption
        {
            get => PowerConsumptionModifier.GetValue(controllerData.powerConsumption);
        }

        public virtual float PowerStorage
        {
            get => PowerStorageModifier.GetValue(controllerData.powerStorage);
        }

        public virtual float TurningPower
        {
            get => TurningPowerModifier.GetValue(controllerData.turningRate);
        }

        public GridTotals Clone()
        {
            return new GridTotals()
            {
                controllerData = controllerData,
                mass = mass,
                powerGenerationModifier = powerGenerationModifier,
                turningPowerModifier = turningPowerModifier,
                hull = hull
            };
        }

        public IEnumerable<ITooltipComponent> GetDebugTooltipComponents() => new ITooltipComponent[]
        {
            new TooltipText($"Mass: {Mass}"),
            new TooltipText($"Hull: {Hull}"),
            new TooltipText($"PeakPowerGeneration: {PowerGenerationModifier}"),
            new TooltipText($"TurningPower: {TurningPowerModifier}"),
        };
    }
}