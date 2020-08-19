namespace Exa.Grids.Blocks.Components
{
    public interface IThruster : IBehaviourMarker<ThrusterData>
    {
    }

    public class ThrusterBehaviour : BlockBehaviour<ThrusterData>
    {
        protected override void OnAdd()
        {
            ship.blockGrid.ThrustVectors.Register(this, data.newtonThrust);
        }

        protected override void OnRemove()
        {
            ship.blockGrid.ThrustVectors.Unregister(this, data.newtonThrust);
        }
    }
}