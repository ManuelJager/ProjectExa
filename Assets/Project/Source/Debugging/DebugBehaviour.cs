using Exa.Debugging.Commands;
using UnityEngine;

namespace Exa.Debugging
{
    public abstract class DebugBehaviour : MonoBehaviour
    {
        public abstract void OnDebugStateChange(DebugMode mode);

        protected virtual void Awake()
        {
            Systems.DebugChange += OnDebugStateChange;
        }
    }
}