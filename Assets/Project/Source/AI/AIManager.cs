using System.Collections.Generic;
using UnityEngine;

namespace Exa.AI
{
    public class AiManager : MonoBehaviour
    {
        [SerializeField] private readonly int _ticksPerSecond = 10;
        private float _elapsedSinceLastTick = 0f;
        private float _updateDeltaThreshold;
        private readonly List<IAgent> _agents = new List<IAgent>();

        private void Awake()
        {
            _updateDeltaThreshold = 1f / _ticksPerSecond;
        }

        public void Register(IAgent agent)
        {
            _agents.Add(agent);
        }

        public void Unregister(IAgent agent)
        {
            _agents.Remove(agent);
        }

        private void Update()
        {
            if (_elapsedSinceLastTick > _updateDeltaThreshold)
            {
                foreach (var agent in _agents)
                {
                    agent.AiUpdate();
                }
                _elapsedSinceLastTick = 0f;
            }

            _elapsedSinceLastTick += Time.deltaTime;
        }
    }
}