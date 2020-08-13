using Exa.Generics;
using Exa.Utils;
using System.Text;

namespace Exa.Grids
{
    public class GridTotals : ICloneable<GridTotals>
    {
        private long mass;
        private float hull;
        private float peakPowerGeneration;
        private float turningPower;

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

        public virtual float PeakPowerGeneration
        {
            get => peakPowerGeneration;
            set => peakPowerGeneration = value;
        }

        public virtual float TurningPower
        {
            get => turningPower;
            set => turningPower = value;
        }

        public GridTotals Clone()
        {
            return new GridTotals()
            {
                mass = mass,
                peakPowerGeneration = peakPowerGeneration,
                turningPower = turningPower,
                hull = hull
            };
        }

        public string ToString(int tabs = 0)
        {
            var sb = new StringBuilder();
            sb.AppendLineIndented($"Mass: {Mass}", tabs);
            sb.AppendLineIndented($"Hull: {Hull}", tabs);
            sb.AppendLineIndented($"PeakPowerGeneration: {PeakPowerGeneration}", tabs);
            sb.AppendLineIndented($"TurningPower: {TurningPower}", tabs);
            return sb.ToString();
        }
    }
}