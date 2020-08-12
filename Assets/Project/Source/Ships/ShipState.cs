using Exa.Utils;
using System.Text;
using UnityEngine;

namespace Exa.Ships
{
    public class ShipState : MonoBehaviour
    {
        public Ship ship;

        private float hull;

        public float TotalMass { get; set; }
        public float TotalHull { get; set; }
        public float CurrentHull { get; set; }
        public float Hull
        {
            get => hull;
            set
            {
                hull = value;
                ship.overlay.overlayHullBar.SetFill(value);
            }
        }
        public float TotalTurningPower { get; set; }
        public float TurningRate
        {
            get => TotalTurningPower * 1000 / TotalMass;
        }

        public void Update()
        {
            ship.navigation.SetTurningMultiplier(TurningRate);
            Hull = CurrentHull / TotalHull;
        }

        public string ToString(int tabs = 0)
        {
            var sb = new StringBuilder();
            sb.AppendLineIndented($"TotalMass: {TotalMass}", tabs);
            sb.AppendLineIndented($"TotalHull: {TotalHull}", tabs);
            sb.AppendLineIndented($"CurrentHull: {CurrentHull}", tabs);
            sb.AppendLineIndented($"Hull: {Hull}", tabs);
            sb.AppendLineIndented($"TotalTurningPower: {TotalTurningPower}", tabs);
            sb.AppendLineIndented($"TurningRate: {TurningRate}", tabs);
            return sb.ToString();
        }
    }
}