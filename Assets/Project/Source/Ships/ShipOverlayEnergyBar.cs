using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.Ships
{
    public class ShipOverlayEnergyBar : MonoBehaviour
    {
        [SerializeField] private Image image;

        public void SetFill(float fill)
        {
            image.fillAmount = fill;
        }
    }
}