using Exa.UI.Tooltips;
using Exa.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Exa.Ships
{
    public class ShipState : MonoBehaviour
    {
        public Ship ship;

        private float hullIntegrity;

        public float HullIntegrity
        {
            get => hullIntegrity;
            set
            {
                hullIntegrity = value;
                ship.overlay.overlayHullBar.SetFill(value);
            }
        }

        public float TurningRate
        {
            get => ship.blockGrid.Totals.TurningPower * 1000 / ship.blockGrid.Totals.Mass;
        }

        public void Update()
        {
            var currentHull = ship.blockGrid.Totals.Hull;
            var totalHull = ship.Blueprint.Blocks.Totals.Hull;
            HullIntegrity = currentHull / totalHull; 
        }

        public IEnumerable<ITooltipComponent> GetDebugTooltipComponents() => new ITooltipComponent[]
        {
            new TooltipText($"HullIntegrity: {HullIntegrity}"),
            new TooltipText($"TurningRate: {TurningRate}")
        };
    }
}