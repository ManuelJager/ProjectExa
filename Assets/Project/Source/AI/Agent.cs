using UnityEngine;

namespace Exa.AI
{
    public abstract class Agent : MonoBehaviour, IAgent
    {
        protected virtual void OnEnable()
        {
            GameSystems.AI.Register(this);
        }

        protected virtual void OnDisable()
        {
            GameSystems.AI.Unregister(this);
        }

        public abstract void AIUpdate();
    }
}