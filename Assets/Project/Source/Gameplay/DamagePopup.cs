using DG.Tweening;
using Exa.UI.Tweening;
using TMPro;
using UnityEngine;

namespace Exa.Gameplay
{
    public class DamagePopup : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshPro tmp;

        [Header("Settings")]
        [SerializeField] private AnimationCurve scaleAnimationCurve;
        [SerializeField] private AnimationCurve positionAnimationCurve;
        [SerializeField] private float maxLifetime;
        [SerializeField] private float scaleMultiplier;
        [SerializeField] private float positionMultiplier;

        private float currentLifetime;
        private Vector2 worldPosition;

        public void Setup(Vector2 worldPosition, string text) {
            SetScale(scaleAnimationCurve.Evaluate(0f));
            transform.position = worldPosition;
            this.worldPosition = worldPosition;
            tmp.SetText(text);
        }

        public void Update() {
            currentLifetime += Time.deltaTime;

            if (currentLifetime > maxLifetime) {
                Destroy(gameObject);
                return;
            }

            var time = currentLifetime / maxLifetime;
            SetPosition(time);
            SetScale(scaleAnimationCurve.Evaluate(time));
        }

        private void SetScale(float scale) {
            scale *= scaleMultiplier;
            transform.localScale = new Vector3(scale, scale);
        }

        private void SetPosition(float time) {
            var evaluatedPosition = positionAnimationCurve.Evaluate(time) * positionMultiplier;
            transform.localPosition = worldPosition + new Vector2(0f, evaluatedPosition - 1f);
        }
    }
}