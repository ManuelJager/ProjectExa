using System.Collections.Generic;
using UnityEngine;

namespace Exa.AI
{
    public class AIManager : MonoBehaviour
    {
        [SerializeField] private int ticksPerSecond = 10;
        private float elapsedSinceLastTick = 0f;
        private float updateDeltaThreshold;
        private List<IAgent> agents = new List<IAgent>();

        private void Awake()
        {
            updateDeltaThreshold = 1f / ticksPerSecond;
        }

        public void Register(IAgent agent)
        {
            agents.Add(agent);
        }

        public void Unregister(IAgent agent)
        {
            agents.Remove(agent);
        }

        private void Update()
        {
            if (elapsedSinceLastTick > updateDeltaThreshold)
            {
                foreach (var agent in agents)
                {
                    agent.AIUpdate();
                }
                elapsedSinceLastTick = 0f;
            }

            elapsedSinceLastTick += Time.deltaTime;
        }
    }
}