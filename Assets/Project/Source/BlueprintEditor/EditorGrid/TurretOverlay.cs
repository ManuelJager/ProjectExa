using Exa.Grids.Blocks.Components;
using Exa.Math;
using Exa.UI.Tweening;
using Exa.Utils;
using UnityEngine;

namespace Exa.ShipEditor
{
    public class TurretOverlay : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ExaEase ease;

        public Color Color {
            set => spriteRenderer.color = value;
        }

        public void Generate(ITurretValues values) {
            spriteRenderer.sprite = GenerateTexture(values).CreateSprite();
            gameObject.SetActive(false);
        }

        private Texture2D GenerateTexture(ITurretValues template) {
            var pixelRadius = Mathf.RoundToInt(template.TurretRadius * 32);
            var size = pixelRadius * 2;
            var centre = (pixelRadius - 0.5f).ToVector2();
            var arc = template.TurretArc;
            return new Texture2D(size, size).SetDefaults()
                .DrawCone(Color.white.SetAlpha(1f), centre, pixelRadius, arc)
                .DrawFadingCone(Color.white.SetAlpha(0.8f), centre, pixelRadius - 1.2f, arc, ease.Evaluate);
        }
    }
}