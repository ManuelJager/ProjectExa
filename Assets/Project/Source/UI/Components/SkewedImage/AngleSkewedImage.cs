using Exa.Math;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI
{
    public class AngleSkewedImage : PixelSkewedImage
    {
        private Vector2 pixelSkew;
        
        protected override void Awake() {
            base.Awake();
            RecalculatePixels();
        }

        public void RecalculatePixels() {
            var r = GetPixelAdjustedRect();
            pixelSkew = new Vector2 {
                x = new Vector2(0, r.height / 2).Rotate(-skew.x).x,
                y = new Vector2(r.width / 2, 0).Rotate(-skew.y).y
            };
        }

        protected override Vector2 GetPixelSkew() {
            return pixelSkew;
        }
    }
}