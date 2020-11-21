using Exa.UI.Tooltips;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Ships
{
    public class ShipState : MonoBehaviour
    {
        public Ship ship;

        private float hullIntegrity;

        public float HullIntegrity {
            get => hullIntegrity;
            set {
                hullIntegrity = value;
                ship.Overlay.overlayHullBar.SetFill(value);
            }
        }

        public void Update() {
            var currentHull = ship.BlockGrid.Totals.Hull;
            var totalHull = ship.Blueprint.Blocks.Totals.Hull;
            HullIntegrity = currentHull / totalHull;
        }

        public float GetTurningRate() {
            return ship.BlockGrid.Totals.TurningPower / ship.BlockGrid.Totals.Mass;
        }

        public IEnumerable<ITooltipComponent> GetDebugTooltipComponents() => new ITooltipComponent[] {
            new TooltipText($"HullIntegrity: {HullIntegrity}"),
            new TooltipText($"TurningRate: {GetTurningRate()}")
        };
    }
}