using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.Ships
{
    public class ShipOverlayEnergyBar : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public void SetFill(float fill)
        {
            _image.fillAmount = fill;
        }
    }
}