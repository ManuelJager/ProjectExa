using Exa.Grids.Blocks.BlockTypes;
using Exa.Math;
using Exa.Utils;
using UnityEngine;

namespace Exa.ShipEditor
{
    public class TurretOverlay : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        public Color Color {
            set => spriteRenderer.color = value;
        }

        public void Import(ITurretTemplate template) {
            spriteRenderer.sprite = GenerateTexture(template).CreateSprite();
            Color = Color.white;
            gameObject.SetActive(false);
        }

        private static Texture2D GenerateTexture(ITurretTemplate template) {
            var pixelRadius = Mathf.RoundToInt(template.TurretRadius * 32);
            var size = pixelRadius * 2;
            var centre = (pixelRadius - 0.5f).ToVector2();
            return new Texture2D(size, size).SetDefaults()
                .DrawCircle(new Color(1, 1, 1, 0.5f), centre, pixelRadius, true)
                .DrawCircle(new Color(1, 1, 1, 0.2f), centre, pixelRadius - 2);
        }
    }
}