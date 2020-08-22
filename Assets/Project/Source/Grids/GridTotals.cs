using Exa.Data;
using Exa.Generics;
using Exa.Grids.Blocks.Components;
using Exa.Utils;
using System.Text;

namespace Exa.Grids
{
    public class GridTotals : ICloneable<GridTotals>
    {
        public ControllerData controllerData;

        private long mass;
        private float hull;
        private Percentage powerGenerationModifier;
        private Percentage peakPowerConsumptionModifier;
        private Percentage powerStorageModifier;
        private Percentage turningPowerModifier;

        public long Mass
        {
            get => mass;
            set => mass = value;
        }

        public float Hull
        {
            get => hull;
            set => hull = value;
        }

        public Percentage PowerGenerationModifier
        {
            get => powerGenerationModifier;
            set => powerGenerationModifier = value;
        }

        public Percentage PowerConsumptionModifier
        {
            get => peakPowerConsumptionModifier;
            set => peakPowerConsumptionModifier = value;
        }

        public Percentage PowerStorageModifier
        {
            get => powerStorageModifier;
            set => powerStorageModifier = value;
        }

        public Percentage TurningPowerModifier
        {
            get => turningPowerModifier;
            set => turningPowerModifier = value;
        }

        public float PowerGeneration
        {
            get => PowerGenerationModifier.GetValue(controllerData.powerGeneration);
        }

        public float PowerConsumption
        {
            get => PowerConsumptionModifier.GetValue(controllerData.powerConsumption);
        }

        public float PowerStorage
        {
            get => PowerStorageModifier.GetValue(controllerData.powerStorage);
        }

        public float TurningPower
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

        public string ToString(int tabs = 0)
        {
            var sb = new StringBuilder();
            sb.AppendLineIndented($"Mass: {Mass}", tabs);
            sb.AppendLineIndented($"Hull: {Hull}", tabs);
            sb.AppendLineIndented($"PeakPowerGeneration: {PowerGenerationModifier}", tabs);
            sb.AppendLineIndented($"TurningPower: {TurningPowerModifier}", tabs);
            return sb.ToString();
        }
    }
}