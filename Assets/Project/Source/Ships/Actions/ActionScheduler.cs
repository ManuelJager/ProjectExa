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
        private readonly Ship ship;
        private readonly List<ShipAction> shipActions = new List<ShipAction>();
        private readonly float generation;
        private float stored;
        private readonly float storage;

        public ActionScheduler(Ship ship)
        {
            this.ship = ship;
            this.generation = 50f;
            this.stored = 500f;
            this.storage = 500f;
        }

        public void Add(ShipAction shipAction)
        {
            shipActions.Add(shipAction);
        }

        public void Remove(ShipAction shipAction)
        {
            shipActions.Remove(shipAction);
        }

        public void ExecuteActions(float deltaTime)
        {
            stored += generation * deltaTime;

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
                stored -= energyTaken;
            }

            if (stored < 0)
            {
                Debug.Log("Current energy level below 0. This should never happen");
            }

            if (stored > storage)
            {
                stored = storage;
            }

            ship.overlay.overlayEnergyBar.SetFill(stored / storage);
        }

        /// <summary>
        /// Tries to take a given amount of energy 
        /// </summary>
        /// <param name="energy"></param>
        /// <returns>The amount of energy taken</returns>
        private float TryTake(float energy)
        {
            return stored - Mathf.Clamp(stored - energy, 0, float.MaxValue);
        }

        private IEnumerable<ActionCache> SortActions(float deltaTime)
        {
            return shipActions
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
