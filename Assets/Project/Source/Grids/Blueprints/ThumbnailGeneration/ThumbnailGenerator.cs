using Exa.Grids.Blueprints.Editor;
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
                Destroy(child.gameObject);
            }

            return tex;
        }

        private void GenerateShip(Blueprint blueprint)
        {
            foreach (var block in blueprint.Blocks.anchoredBlueprintBlocks)
            {
                CreateBlock(block.blueprintBlock, block.gridAnchor);
            }
        }

        private void CreateBlock(BlueprintBlock block, Vector2Int gridAnchor)
        {
            var prefab = MainManager.Instance.blockFactory.GetBlock(block.id);

            var blockGO = Instantiate(prefab, transform);

            var spriteRenderer = blockGO.GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = block.flippedX;
            spriteRenderer.flipY = block.flippedY;
            blockGO.transform.SetParent(transform);
            blockGO.transform.localRotation = block.QuaternionRotation;
            blockGO.transform.localPosition = ShipEditorUtils.GetRealPositionByAnchor(block, gridAnchor);
        }
    }
}