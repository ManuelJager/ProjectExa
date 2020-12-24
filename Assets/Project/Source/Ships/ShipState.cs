using Exa.UI.Tooltips;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Ships
{
    public class ShipState : MonoBehaviour
    {
        public GridInstance gridInstance;

        private float hullIntegrity;

        public float HullIntegrity {
            get => hullIntegrity;
            set {
                hullIntegrity = value;

                if (gridInstance.Active)
                    gridInstance.Overlay.overlayHullBar.SetFill(value);
            }
        }

        public void Update() {
            var currentHull = gridInstance.BlockGrid.Totals.Hull;
            var totalHull = gridInstance.Blueprint.Blocks.Totals.Hull;
            HullIntegrity = currentHull / totalHull;
        }

        public float GetTurningRate() {
            return gridInstance.BlockGrid.Totals.TurningPower / gridInstance.BlockGrid.Totals.Mass;
        }

        public IEnumerable<ITooltipComponent> GetDebugTooltipComponents() => new ITooltipComponent[] {
            new TooltipText($"HullIntegrity: {HullIntegrity}"),
            new TooltipText($"TurningRate: {GetTurningRate()}")
        };
    }
}