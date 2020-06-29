using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Exa.Grids.Blueprints.BlueprintEditor
{
    public class EditorGridBlueprintLayer : MonoBehaviour
    {
        public UnityEvent onBlueprintChanged;

        [SerializeField] private ShipEditorStopwatch stopwatch;
        private Dictionary<Vector2Int, GameObject> blocksByBlueprintAnchor = new Dictionary<Vector2Int, GameObject>();

        public Blueprint ActiveBlueprint { get; private set; }

        public void Import(Blueprint blueprint)
        {
            ActiveBlueprint = blueprint;
            blocksByBlueprintAnchor = new Dictionary<Vector2Int, GameObject>();

            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            foreach (var block in blueprint.Blocks.anchoredBlueprintBlocks)
            {
                PlaceBlock(block);
            }
        }

        public void AddBlock(AnchoredBlueprintBlock anchoredBlueprintBlock)
        {
            // Reset the stopwatch timer used by the shipeditor to time blueprint grid validation
            stopwatch.Reset();
            onBlueprintChanged?.Invoke();
            PlaceBlock(anchoredBlueprintBlock);
            ActiveBlueprint.Add(anchoredBlueprintBlock);
        }

        public void RemoveBlock(Vector2Int gridPos)
        {
            if (!ActiveBlueprint.Blocks.HasOverlap(gridPos)) return;

            // Reset the stopwatch timer used by the shipeditor to time blueprint grid validation
            stopwatch.Reset();

            var anchoredBlock = ActiveBlueprint.Blocks.GetAnchoredBlockAtGridPos(gridPos);
            var anchoredPos = anchoredBlock.gridAnchor;

            ActiveBlueprint.Remove(anchoredPos);
            onBlueprintChanged?.Invoke();
            Destroy(blocksByBlueprintAnchor[anchoredPos]);
        }

        public void ClearBlueprint()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            ActiveBlueprint.ClearBlocks();
        }

        public void PlaceBlock(AnchoredBlueprintBlock anchoredBlueprintBlock)
        {
            // Create block
            var block = CreateBlock(anchoredBlueprintBlock.blueprintBlock);
            // Keep track of block object by grid anchor position
            blocksByBlueprintAnchor[anchoredBlueprintBlock.gridAnchor] = block;
            // Set position of block
            var position = ShipEditorUtils.GetRealPositionByAnchor(
                anchoredBlueprintBlock.blueprintBlock, 
                anchoredBlueprintBlock.gridAnchor);

            block.transform.localPosition = position;
        }

        public Blueprint Export()
        {
            return ActiveBlueprint;
        }

        private GameObject CreateBlock(BlueprintBlock block)
        {
            var blockObject = new GameObject();
            var spriteRenderer = blockObject.AddComponent<SpriteRenderer>();
            spriteRenderer.flipX = block.flippedX;
            spriteRenderer.flipY = block.flippedY;
            spriteRenderer.sprite = block.RuntimeContext.Thumbnail;
            blockObject.transform.SetParent(transform);
            blockObject.transform.localRotation = block.QuaternionRotation;
            return blockObject;
        }
    }
}