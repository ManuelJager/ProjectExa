using UnityEngine;

namespace Exa.Gameplay {
    public class SpawnLayer : MonoBehaviour {
        public Transform overlay;
        public Transform projectiles;
        public Transform ships;
        public Transform drones;

        public void SetLayerActive(bool active) {
            overlay.gameObject.SetActive(active);
            projectiles.gameObject.SetActive(active);
            ships.gameObject.SetActive(active);
            drones.gameObject.SetActive(active);
        }
    }
}