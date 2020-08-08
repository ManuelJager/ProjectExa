using Exa.Debugging.Commands;
using UnityEngine;

namespace Exa.Debugging
{
    public abstract class DebugBehaviour : MonoBehaviour
    {
        public abstract void OnDebugStateChange(bool state);

        protected virtual void Awake()
        {
            Systems.DebugChange += OnDebugStateChange;
        }
    }
}