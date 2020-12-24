using System.Collections;
using System.Collections.Generic;
using Exa.Ships;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.Gameplay
{
    public class CoreHealthBar : MonoBehaviour
    {
        [SerializeField] private RectTransform bar;
        [SerializeField] private Text maxHullText;
        [SerializeField] private Text currentHullText;
        private GridInstance instance;

        public void TrackHealth(GridInstance instance) {
            this.instance = instance;
        }

        public void Update() {
            if (instance == null) return;

            var physicalBehaviour = instance.Controller.PhysicalBehaviour;
            var currentHull = physicalBehaviour.Data.hull;
            var maxHull = physicalBehaviour.GetDefaultData().hull;
            var integrity = currentHull / maxHull;

            bar.localScale = new Vector3(integrity, 1, 1);
            currentHullText.text = Mathf.RoundToInt(currentHull).ToString();
            maxHullText.text = Mathf.RoundToInt(maxHull).ToString();
        }
    }
}
