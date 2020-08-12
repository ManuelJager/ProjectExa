namespace Exa.Grids.Blocks.Components
{
    public interface IGyroscope
    {
        GyroscopeBehaviour GyroscopeBehaviour { get; }
    }

    public class GyroscopeBehaviour : BlockBehaviour<GyroscopeData>
    {
        protected override void OnAdd()
        {
            ship.state.TotalTurningPower += data.turningRate;
        }

        protected override void OnRemove()
        {
            ship.state.TotalTurningPower -= data.turningRate;
        }
    }
}