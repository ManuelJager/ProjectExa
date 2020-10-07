using UnityEngine;

namespace Exa.AI
{
    public abstract class Agent : MonoBehaviour, IAgent
    {
        protected virtual void Start()
        {
            GameSystems.Ai.Register(this);
        }

        protected virtual void OnDestroy()
        {
            GameSystems.Ai.Unregister(this);
        }

        public abstract void AiUpdate();
    }
}