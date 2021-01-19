using Exa.Utils;
using UnityEngine;

namespace Exa.ShipEditor
{
    public class TurretOverlayHandle
    {
        public bool Collides { get; set; }
        public TurretOverlay Overlay { get; }

        public TurretOverlayHandle(TurretOverlay overlay) {
            this.Overlay = overlay;
            overlay.ConfigureAsGhostOverlay(this);
        }

        public void Destroy() {
            Object.Destroy(Overlay.gameObject);
        }

        public void SetActive(bool value) {
            Overlay.gameObject.SetActive(value);
        }
    }
}