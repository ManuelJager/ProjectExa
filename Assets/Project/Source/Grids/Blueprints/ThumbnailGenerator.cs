﻿using Exa.Utils;
using UnityEngine;

namespace Exa.Grids.Blueprints
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

        public void GenerateThumbnail(Blueprint blueprint)
        {
            // Generate ship
            foreach (var block in blueprint.Blocks)
            {
                var blockGO = block.CreateInactiveInertBlockInGrid(transform);
                blockGO.SetActive(true);
            }

            RuntimePreviewGenerator.PreviewDirection = transform.forward;
            var tex = RuntimePreviewGenerator.GenerateModelPreview(transform, 512, 512, false);
            blueprint.Thumbnail = tex;

            // Cleaup ship
            foreach (var child in transform.GetChildren())
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}