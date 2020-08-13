using Exa.Utils;
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
            ship.navigation.SetTurningMultiplier(TurningRate);
            var currentHull = ship.blockGrid.Totals.Hull;
            var totalHull = ship.Blueprint.Blocks.Totals.Hull;
            HullIntegrity = currentHull / totalHull; 
        }

        public string ToString(int tabs = 0)
        {
            var sb = new StringBuilder();
            sb.AppendLineIndented($"HullIntegrity: {HullIntegrity}", tabs);
            sb.AppendLineIndented($"TurningRate: {TurningRate}", tabs);
            return sb.ToString();
        }
    }
}