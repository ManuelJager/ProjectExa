using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Exa.Data;
using Exa.Utils;
using UnityEngine;
#pragma warning disable CS0649

namespace Exa.UI.Components
{
    public class Border : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private CanvasGroup canvasGroup;

        [Header("Settings")] 
        [SerializeField] private ActivePair<float> imageAlpha = new ActivePair<float>(1f, 0f);
        [SerializeField] private float animTime = 0.1f;

        private Tween alphaTween;

        public void Show()
        {
            gameObject.SetActive(true);
            canvasGroup.DOFade(imageAlpha.active, animTime)
                .Replace(ref alphaTween);
        }

        public void Hide()
        {
            canvasGroup.DOFade(imageAlpha.inactive, animTime)
                .Replace(ref alphaTween)
                .OnComplete(() => gameObject.SetActive(false));
        }

        public void SetVisibility(bool value)
        {
            if (value) Show();
            else Hide();
        }
    }
}

