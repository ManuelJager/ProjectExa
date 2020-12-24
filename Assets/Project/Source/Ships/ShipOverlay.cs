using UnityEngine;

namespace Exa.Ships
{
    public class ShipOverlay : MonoBehaviour
    {
        public GridInstance gridInstance;

        public RectTransform rectContainer;
        public ShipOverlayHullBar overlayHullBar;
        public ShipOverlayEnergyBar overlayEnergyBar;
        public ShipOverlayCircle overlayCircle;

        private void Update() {
            transform.position = gridInstance.GetPosition();
        }
    }
}