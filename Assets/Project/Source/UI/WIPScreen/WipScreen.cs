using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Exa.Data;
using Exa.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI
{
    public class WipScreen : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CanvasGroup rootCanvasGroup;
        [SerializeField] private CanvasGroup textCanvasGroup;
        [SerializeField] private GameObject wordPrefab;
        [SerializeField] private RectTransform textContainer;
        [SerializeField] private RectTransform wordContainer;
        [SerializeField] private HorizontalLayoutGroup wordLayoutGroup;

        [Header("Settings")]
        [SerializeField] private bool forceShowScreen;
        [SerializeField] private AnimSettings animSettings;

        public void Init()
        {
            if (!Debug.isDebugBuild || forceShowScreen)
                ShowScreen();
        }

        private void ShowScreen()
        {
            gameObject.SetActive(true);
            rootCanvasGroup.alpha = 1f;
            rootCanvasGroup.blocksRaycasts = true;
            this.Delay(FadeOut, animSettings.screenDuration);

            textCanvasGroup.alpha = 0f;
            this.Delay(FadeInText, animSettings.textFadeInDelay);

            var scale = animSettings.textTargetScale;
            textContainer.DOScale(new Vector3(scale, scale, 1), animSettings.screenDuration);

            foreach (var word in animSettings.message.Split(' '))
            {
                Instantiate(wordPrefab, wordContainer).GetComponent<Text>().text = word;
            }
        }

        private void FadeOut()
        {
            rootCanvasGroup.blocksRaycasts = false;
            rootCanvasGroup.DOFade(0f, animSettings.fadeOutDuration);
        }

        private void FadeInText()
        {
            textCanvasGroup.DOFade(1f, animSettings.textFadeInDuration);
            wordLayoutGroup.DOSpacing(24f, animSettings.screenDuration + animSettings.fadeOutDuration);
        }

        [Serializable]
        public struct AnimSettings
        {
            public float textFadeInDelay;
            public float textFadeInDuration;
            public float textTargetScale;
            public float screenDuration;
            public float fadeOutDuration;
            public string message;
        }
    }
}

