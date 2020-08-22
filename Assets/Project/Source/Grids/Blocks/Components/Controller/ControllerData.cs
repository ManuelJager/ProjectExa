namespace Exa.Grids.Blocks.Components
{
    public struct ControllerData : IBlockComponentData
    {
        public float powerGeneration;
        public float powerConsumption;
        public float powerStorage;
        public float turningRate;

        public void AddGridTotals(GridTotals totals)
        {
            totals.controllerData = this;
        }

        public void RemoveGridTotals(GridTotals totals)
        {
        }
    }
}