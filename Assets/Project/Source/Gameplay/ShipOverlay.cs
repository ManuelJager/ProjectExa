using Exa.Grids.Ships;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.Gameplay
{
    public class ShipOverlay : MonoBehaviour
    {
        public Ship ship;
        public Canvas canvas;
        public Image image;

        private void Update()
        {
            transform.position = ship.transform.position;
        }

        public void SetFill(float value)
        {
            image.fillAmount = value;
        }
    }
}