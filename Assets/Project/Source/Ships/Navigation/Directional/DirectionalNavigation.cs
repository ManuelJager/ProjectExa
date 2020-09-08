using Exa.Ships.Targetting;

namespace Exa.Ships.Navigation
{
    public class DirectionalNavigation : INavigation
    {
        private readonly Ship ship;
        private readonly NavigationOptions options;
        private readonly AxisThrustVectors thrustVectors;

        public IThrustVectors ThrustVectors => thrustVectors;

        public DirectionalNavigation(Ship ship, NavigationOptions options, float directionalThrust)
        {
            this.ship = ship;
            this.options = options;

            thrustVectors = new AxisThrustVectors(directionalThrust);
        }

        public void ScheduledFixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public void SetLookAt(ITarget target)
        {
            throw new System.NotImplementedException();
        }

        public void SetMoveTo(ITarget target)
        {
            throw new System.NotImplementedException();
        }
    }
}