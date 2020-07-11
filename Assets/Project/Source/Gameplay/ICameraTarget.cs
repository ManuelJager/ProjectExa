using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Exa.Gameplay
{
    public interface ICameraTarget
    {
        Vector3 GetWorldPosition();
    }
}
