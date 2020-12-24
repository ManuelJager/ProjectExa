using UnityEngine;

namespace Exa.Debugging
{
    public abstract class DebugBehaviour : MonoBehaviour
    {
        public abstract void OnDebugStateChange(DebugMode mode);

        protected virtual void Awake() {
            DebugManager.DebugChange += OnDebugStateChange;
        }
    }
}