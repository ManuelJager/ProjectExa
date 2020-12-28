using Exa.Grids.Blocks.Components;

namespace Exa.Research
{
    public class ResearchStep<T> : ResearchStep
        where T : struct, IBlockComponentValues
    {
        public delegate void ApplyValues(T initialValues, ref T currentValues);

        private ApplyValues applyFunc;

        public ResearchStep(ApplyValues applyFunc, ValueModificationOrder order)
            : base(order) {
            this.applyFunc = applyFunc;
        }

        public override IBlockComponentValues CalculateCurrentValues(IBlockComponentValues initial, IBlockComponentValues current) {
            var convertedCopy = (T) current;
            applyFunc((T) initial, ref convertedCopy);
            return convertedCopy;
        }

        public override bool MatchesType(IBlockComponentValues values) {
            return values.GetType() == typeof(T);
        }
    }

    public abstract class ResearchStep
    {
        protected ResearchStep(ValueModificationOrder order) {
            this.Order = order;
        }

        public ValueModificationOrder Order { get; }

        public abstract IBlockComponentValues CalculateCurrentValues(IBlockComponentValues initial, IBlockComponentValues current);

        public abstract bool MatchesType(IBlockComponentValues values);
    }
}