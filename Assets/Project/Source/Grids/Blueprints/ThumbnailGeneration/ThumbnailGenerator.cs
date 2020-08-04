using Exa.Utils;
using UnityEngine;

namespace Exa.Grids.Blueprints.Thumbnails
{
    public class ThumbnailGenerator : MonoBehaviour
    {
        [SerializeField] private Color backgroundColor;
        [SerializeField] private float padding;

        private void Awake()
        {
            RuntimePreviewGenerator.Padding = padding;
            RuntimePreviewGenerator.OrthographicMode = true;
            RuntimePreviewGenerator.BackgroundColor = backgroundColor;
            RuntimePreviewGenerator.MarkTextureNonReadable = false;
        }

        public Texture2D GenerateThumbnail(Blueprint blueprint)
        {
            GenerateShip(blueprint);
            RuntimePreviewGenerator.PreviewDirection = transform.forward;
            var tex = RuntimePreviewGenerator.GenerateModelPreview(transform, 512, 512, false);

            foreach (var child in transform.GetChildren())
            {
                child.gameObject.SetActive(false);
            }

            return tex;
        }

        private void GenerateShip(Blueprint blueprint)
        {
            foreach (var block in blueprint.Blocks)
            {
                block.CreateInertBehaviourInGrid(transform);
            }
        }
    }
}