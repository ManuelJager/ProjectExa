using UnityEngine;

namespace Exa.Ships
{
    public class ShipOverlay : MonoBehaviour
    {
        public Ship ship;

        public RectTransform rectContainer;
        public ShipOverlayHullBar overlayHullBar;
        public ShipOverlayEnergyBar overlayEnergyBar;
        public ShipOverlayCircle overlayCircle;

        private void Update() {
            transform.position = ship.transform.position;
        }
    }
}