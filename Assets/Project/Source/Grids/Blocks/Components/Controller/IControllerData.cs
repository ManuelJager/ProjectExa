namespace Exa.Grids.Blocks.Components
{
    public interface IControllerData : IBlockComponentValues
    {
        public float PowerGeneration { get; }
        public float PowerConsumption { get; }
        public float PowerStorage { get; }
        public float TurningRate { get; }
    }
}