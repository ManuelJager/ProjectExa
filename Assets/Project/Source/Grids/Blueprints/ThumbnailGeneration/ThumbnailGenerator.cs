using Exa.Utils;
using System.Collections;
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

        public IEnumerator GenerateThumbnail(Blueprint blueprint)
        {
            // Generate ship
            foreach (var block in blueprint.Blocks.GridMembers)
            {
                var blockGO = block.CreateInactiveInertBlockInGrid(transform);
                blockGO.SetActive(true);
            }

            yield return null;

            RuntimePreviewGenerator.PreviewDirection = transform.forward;
            var tex = RuntimePreviewGenerator.GenerateModelPreview(transform, 512, 512, false);
            blueprint.Thumbnail = tex;

            yield return null;

            // Cleaup ship
            foreach (var child in transform.GetChildren())
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}