using Exa.Utils;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    public class ThumbnailGenerator : MonoBehaviour
    {
        [SerializeField] private Color _backgroundColor;
        [SerializeField] private float _padding;

        private void Awake()
        {
            RuntimePreviewGenerator.Padding = _padding;
            RuntimePreviewGenerator.OrthographicMode = true;
            RuntimePreviewGenerator.BackgroundColor = _backgroundColor;
            RuntimePreviewGenerator.MarkTextureNonReadable = false;
        }

        public void GenerateThumbnail(Blueprint blueprint)
        {
            // Generate Ship
            foreach (var block in blueprint.Blocks)
            {
                var blockGo = block.CreateInactiveInertBlockInGrid(transform);
                blockGo.SetActive(true);
            }

            RuntimePreviewGenerator.PreviewDirection = transform.forward;
            var tex = RuntimePreviewGenerator.GenerateModelPreview(transform, 512, 512, false);
            blueprint.Thumbnail = tex;

            // Cleaup Ship
            transform.SetActiveChildren(false);
        }
    }
}