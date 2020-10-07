using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Exa.Ships
{
    public class ActionScheduler
    {
        private readonly Ship _ship;
        private readonly List<ShipAction> _shipActions = new List<ShipAction>();
        private readonly float _generation;
        private float _stored;
        private readonly float _storage;

        public ActionScheduler(Ship ship)
        {
            this._ship = ship;
            this._generation = 50f;
            this._stored = 500f;
            this._storage = 500f;
        }

        public void Add(ShipAction shipAction)
        {
            _shipActions.Add(shipAction);
        }

        public void Remove(ShipAction shipAction)
        {
            _shipActions.Remove(shipAction);
        }

        public void ExecuteActions(float deltaTime)
        {
            _stored += _generation * deltaTime;

            foreach (var actionCache in SortActions(deltaTime))
            {
                // Calculate the energy coefficient the action can use
                var energyTaken = TryTake(actionCache.consumption);
                var energyCoefficient = Mathf.Clamp01(energyTaken / actionCache.consumption);

                // This is a result of an action that uses no power and has no storage
                if (float.IsNaN(energyCoefficient))
                {
                    energyCoefficient = 0f;
                }

                actionCache.action.Update(energyCoefficient, deltaTime);
                _stored -= energyTaken;
            }

            if (_stored < 0)
            {
                Debug.Log("Current energy level below 0. This should never happen");
            }

            if (_stored > _storage)
            {
                _stored = _storage;
            }

            _ship.overlay.overlayEnergyBar.SetFill(_stored / _storage);
        }

        /// <summary>
        /// Tries to take a given amount of energy 
        /// </summary>
        /// <param name="energy"></param>
        /// <returns>The amount of energy taken</returns>
        private float TryTake(float energy)
        {
            return _stored - Mathf.Clamp(_stored - energy, 0, float.MaxValue);
        }

        private IEnumerable<ActionCache> SortActions(float deltaTime)
        {
            return _shipActions
                .Select((action) => new ActionCache
                {
                    consumption = action.CalculateConsumption(deltaTime),
                    action = action
                })
                .OrderBy((actionCache) => actionCache.consumption);
        }

        private struct ActionCache
        {
            public float consumption;
            public ShipAction action;
        }
    }
}
