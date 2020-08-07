using System;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public struct GyroscopeData : IBlockComponentData
    {
        public float turningRate;
    }
}