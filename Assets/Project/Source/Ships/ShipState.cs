﻿using Exa.UI.Tooltips;
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

        public void Update()
        {
            var currentHull = ship.blockGrid.Totals.Hull;
            var totalHull = ship.Blueprint.Blocks.Totals.Hull;
            HullIntegrity = currentHull / totalHull; 
        }

        public float GetTurningRate()
        {
            return ship.blockGrid.Totals.TurningPower / ship.blockGrid.Totals.Mass;
        }

        public IEnumerable<ITooltipComponent> GetDebugTooltipComponents() => new ITooltipComponent[]
        {
            new TooltipText($"HullIntegrity: {HullIntegrity}"),
            new TooltipText($"TurningRate: {GetTurningRate()}")
        };
    }
}