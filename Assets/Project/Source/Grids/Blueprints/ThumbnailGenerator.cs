using System;
using Exa.Utils;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Grids.Blueprints {
    public class ThumbnailGenerator : MonoBehaviour {
        [SerializeField] private Color backgroundColor;
        [SerializeField] private float padding;

        private void Awake() {
            RuntimePreviewGenerator.Padding = padding;
            RuntimePreviewGenerator.OrthographicMode = true;
            RuntimePreviewGenerator.BackgroundColor = backgroundColor;
            RuntimePreviewGenerator.MarkTextureNonReadable = false;
        }

        public void GenerateThumbnail(Blueprint blueprint) {
            // Generate Ship
            foreach (var block in blueprint.Grid) {
                var blockGO = block.CreateInactiveInertBlockInGrid(transform);
                blockGO.SetActive(true);
            }

            transform.GetChildren().ForEach(Debug.Log);

            RuntimePreviewGenerator.PreviewDirection = transform.forward;
            var tex = RuntimePreviewGenerator.GenerateModelPreview(transform, 512, 512);

            if (tex == null) {
                throw new Exception("Generated thumbnail is null");
            }

            blueprint.Thumbnail = tex;

            // Cleanup Ship
            transform.SetActiveChildren(false);
        }
    }
}