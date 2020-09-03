using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exa.Math.ControlSystems
{
    [Serializable]
    public struct PdSettings
    {
        public float proportional;
        public float derivitive;

        public static PdSettings operator *(PdSettings first, float second)
        {
            return new PdSettings
            {
                proportional = first.proportional * second,
                derivitive = first.derivitive * second
            };
        } 
    }
}
