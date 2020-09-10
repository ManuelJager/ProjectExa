using Exa.Ships.Targetting;

namespace Exa.Ships.Navigation
{
    public class DirectionalNavigation : INavigation
    {
        private readonly Ship ship;
        private readonly NavigationOptions options;
        private readonly AxisThrustVectors thrustVectors;

        public ITarget LookAt { private get; set; }
        public ITarget MoveTo { private get; set; }
        public IThrustVectors ThrustVectors => thrustVectors;

        public DirectionalNavigation(Ship ship, NavigationOptions options, float directionalThrust)
        {
            this.ship = ship;
            this.options = options;

            thrustVectors = new AxisThrustVectors(directionalThrust);
        }

        public void ScheduledFixedUpdate()
        {
            
        }
    }
}