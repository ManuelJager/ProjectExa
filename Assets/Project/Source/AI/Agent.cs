using Exa.SceneManagement;
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
            if (GameSystems.Instance != null && GameSystems.AI != null && !GameSystems.AI.GetParentSceneIsUnloading())
                GameSystems.AI.Unregister(this);
        }

        public abstract void AIUpdate();
    }
}