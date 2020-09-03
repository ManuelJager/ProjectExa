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
        ThrustVectors ThrustVectors { get; }

        void ScheduledFixedUpdate();
        void SetLookAt(ITarget target);
        void SetMoveTo(ITarget target);
    }
}
