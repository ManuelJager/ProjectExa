using Exa.Ships.Targetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exa.Ships.Navigation
{
    public interface INavigation
    {
        ITarget LookAt { set; }
        ITarget MoveTo { set; }
        IThrustVectors ThrustVectors { get; }

        void Update(float deltaTime);
    }
}
