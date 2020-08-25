using Exa.Generics;
using Exa.Math;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Exa.Grids.Blocks.Components
{
    public interface IThruster : IBehaviourMarker<ThrusterData>
    {
        void Fire(float strength);
    }

    public class ThrusterBehaviour : BlockBehaviour<ThrusterData>
    {
        [Header("References")]
        [SerializeField] private Transform thrusterFlameContainer;
        [SerializeField] private SpriteRenderer thrusterFlame;
        [SerializeField] private Light2D light2D;

        [Header("Settings")]
        [SerializeField] private MinMax xScale;
        [SerializeField] private MinMax yScale;
        [SerializeField] private MinMax lightIntensityScale;

        public void Fire(float strength)
        {
            thrusterFlame.transform.localScale = new Vector2
            {
                x = xScale.Evaluate(strength),
                y = yScale.Evaluate(strength)
            };

            light2D.intensity = lightIntensityScale.Evaluate(strength);
        }

        protected override void OnAdd()
        {
            var blueprintBlock = block.anchoredBlueprintBlock.blueprintBlock;
            blueprintBlock.SetSpriteRendererFlips(thrusterFlame);

            var pos = thrusterFlameContainer.localPosition.ToVector2().Rotate(-blueprintBlock.Rotation);

            pos *= blueprintBlock.FlipVector;

            thrusterFlameContainer.localPosition = pos.Rotate(blueprintBlock.Rotation);
        }
    }
}