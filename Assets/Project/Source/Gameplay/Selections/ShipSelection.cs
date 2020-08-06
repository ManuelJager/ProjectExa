using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Exa.Gameplay
{
    public class ShipSelection
    {
        public bool CanControl { get; protected set; }

        public virtual void MoveTo(Vector2 position)
        {
            throw new NotImplementedException();
        }
    }
}
