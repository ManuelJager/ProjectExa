using System.Collections;
using DG.Tweening;
using Exa.Math;
using Exa.Utils;
using UnityEngine;

namespace Exa.Grids.Blocks.Components {
    public class ShieldGeneratorBehaviour : BlockBehaviour<ShieldGeneratorData> {
        [Header("References")]
        [SerializeField] private SpriteRenderer lights;
        [SerializeField] private ShieldBubble shieldBubble;

        [Header("Settings")]
        [SerializeField] private float lightsAnimationDuration;

        [Header("State")]
        [SerializeField] private HealthPool healthPool;
        [SerializeField] private bool shieldIsRaised;
        private Tween lightsTween;
        private float normalizedLightsAlpha = 1f;

        protected override void OnAdd() {
            shieldBubble.Block = block;
        }

        protected override void OnBlockDataReceived(ShieldGeneratorData oldValues, ShieldGeneratorData newValues) {
            healthPool.health += newValues.health - oldValues.health;
            shieldBubble.SetRadius(newValues.shieldRadius);
        }

        public ReceivedDamage OnReceiveDamage(Damage damage) {
            if (!shieldIsRaised) {
                Debug.LogError("Received shield damage when shield is not raised");

                return new ReceivedDamage();
            }

            if (!healthPool.TakeDamage(damage, 0, out var receivedDamage)) {
                LowerShields();
            }

            return receivedDamage;
        }

        private void LowerShields() {
            shieldIsRaised = false;
            shieldBubble.Lower();
            AnimateLights(0f);
            
            this.Delay(RaiseShields, Data.recoverTime);
        }

        private void RaiseShields() {
            shieldIsRaised = true;
            shieldBubble.Raise();
            healthPool.health = Data.health;
            AnimateLights(1f);
        }

        private float GetNormalizedLightsAlpha() {
            return normalizedLightsAlpha;
        }

        private void SetNormalizedLightsAlpha(float value) {
            normalizedLightsAlpha = value;

            var randomizedAlpha = value + (Random.value - 0.5f) * 0.1f;
            lights.color = lights.color.SetAlpha(randomizedAlpha);
        }

        private void AnimateLights(float alpha) {
            DOTween.To(GetNormalizedLightsAlpha, SetNormalizedLightsAlpha, alpha, lightsAnimationDuration)
                .Replace(ref lightsTween);
        }
    }
}