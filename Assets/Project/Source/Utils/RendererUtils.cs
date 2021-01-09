using UnityEngine;

namespace Exa.Utils
{
    public static class RendererUtils
    {
        public static Material CopyMaterial(this Renderer renderer) {
            var materialCopy = new Material(renderer.material);
            renderer.material = materialCopy;
            return materialCopy;
        }
    }
}