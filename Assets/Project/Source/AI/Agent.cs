using UnityEngine;

namespace Exa.AI
{
    public abstract class Agent : MonoBehaviour, IAgent
    {
        protected virtual void Start()
        {
            GameSystems.AI.Register(this);
        }

        protected virtual void OnDestroy()
        {
            GameSystems.AI.Unregister(this);
        }

        public abstract void AIUpdate();
    }
}