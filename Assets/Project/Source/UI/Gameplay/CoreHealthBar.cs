using System.Collections;
using System.Collections.Generic;
using Exa.Ships;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.Gameplay
{
    public class CoreHealthBar : MonoBehaviour, IGridOverlay
    {
        [SerializeField] private RectTransform bar;
        [SerializeField] private Text maxHullText;
        [SerializeField] private Text currentHullText;

        public void SetEnergyFill(float current, float max) {
            
        }

        public void SetHullFill(float current, float max) {
            bar.localScale = new Vector3(current / max, 1, 1);
            currentHullText.text = Mathf.RoundToInt(current).ToString();
            maxHullText.text = Mathf.RoundToInt(max).ToString();
        }
    }
}
