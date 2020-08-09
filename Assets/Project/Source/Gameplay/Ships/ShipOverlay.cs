using Exa.Grids.Ships;
using Exa.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.Gameplay
{
    public class ShipOverlay : MonoBehaviour
    {
        public Ship ship;
        public RectTransform rectContainer;
        public ShipOverlayHullBar overlayHullBar;
        public ShipOverlayCircle overlayCircle;

        private void Update()
        {
            transform.position = ship.transform.position;
        }
    }
}