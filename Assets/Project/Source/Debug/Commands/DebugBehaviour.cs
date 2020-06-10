using Exa.Debug.Commands;
using UnityEngine;

namespace Exa.Debug
{
    public abstract class DebugBehaviour : MonoBehaviour
    {
        public abstract void OnDebugStateChange(bool state);

        protected virtual void Awake()
        {
            DebugCommand.DebugChange += OnDebugStateChange;
        }
    }
}