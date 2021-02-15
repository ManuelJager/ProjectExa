using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI
{
    public abstract class PixelSkewedImage : Image
    {
        public Vector2 skew;

        protected override void OnPopulateMesh(VertexHelper vh) {
            base.OnPopulateMesh(vh);
            var r = GetPixelAdjustedRect();
            var v = new Vector4(r.x, r.y, r.x + r.width, r.y + r.height);
            vh.Clear();
            var skew = GetPixelSkew();
            vh.AddVert(new Vector3(v.x - skew.x, v.y - skew.y), color, new Vector2(0f, 0f));
            vh.AddVert(new Vector3(v.x + skew.x, v.w - skew.y), color, new Vector2(0f, 1f));
            vh.AddVert(new Vector3(v.z + skew.x, v.w + skew.y), color, new Vector2(1f, 1f));
            vh.AddVert(new Vector3(v.z - skew.x, v.y + skew.y), color, new Vector2(1f, 0f));
            vh.AddTriangle(0, 1, 2);
            vh.AddTriangle(2, 3, 0);
        }

        protected virtual Vector2 GetPixelSkew() {
            return skew;
        }
    }
}