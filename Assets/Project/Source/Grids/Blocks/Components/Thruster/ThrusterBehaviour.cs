using Exa.Generics;
using Exa.Math;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Exa.Grids.Blocks.Components
{
    public class ThrusterBehaviour : BlockBehaviour<ThrusterData>
    {
        [Header("References")]
        [SerializeField] private Transform _thrusterFlameContainer;
        [SerializeField] private SpriteRenderer _thrusterFlame;
        [SerializeField] private Light2D _light2D;

        [Header("Settings")]
        [SerializeField] private FloatMinMax _xScale;
        [SerializeField] private FloatMinMax _yScale;
        [SerializeField] private FloatMinMax _lightIntensityScale;

        public void Fire(float strength)
        {
            _thrusterFlame.transform.localScale = new Vector2
            {
                x = _xScale.Evaluate(strength),
                y = _yScale.Evaluate(strength)
            };

            _light2D.intensity = _lightIntensityScale.Evaluate(strength);
        }

        protected override void OnAdd()
        {
            var blueprintBlock = block.anchoredBlueprintBlock.blueprintBlock;
            blueprintBlock.SetSpriteRendererFlips(_thrusterFlame);

            var pos = _thrusterFlameContainer.localPosition.ToVector2().Rotate(-blueprintBlock.Rotation);

            pos *= blueprintBlock.FlipVector;

            _thrusterFlameContainer.localPosition = pos.Rotate(blueprintBlock.Rotation);
        }
    }
}