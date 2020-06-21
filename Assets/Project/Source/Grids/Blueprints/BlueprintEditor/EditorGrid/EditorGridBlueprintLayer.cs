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

            foreach (var block in blueprint.blocks)
            {
                PlaceBlock(block.Key, block.Value);
            }
        }

        public void AddBlock(Vector2Int gridPos, BlueprintBlock blueprintBlock)
        {
            // Reset the stopwatch timer used by the shipeditor to time blueprint grid validation
            stopwatch.Reset();
            onBlueprintChanged?.Invoke();
            PlaceBlock(gridPos, blueprintBlock);
            ActiveBlueprint.blocks.Add(gridPos, blueprintBlock);
        }

        public void RemoveBlock(Vector2Int gridPos)
        {
            if (!ActiveBlueprint.blocks.HasOverlap(gridPos)) return;

            // Reset the stopwatch timer used by the shipeditor to time blueprint grid validation
            stopwatch.Reset();

            var anchoredBlock = ActiveBlueprint.blocks.GetAnchoredBlockAtGridPos(gridPos);
            var anchoredPos = anchoredBlock.gridAnchor;

            ActiveBlueprint.blocks.Remove(anchoredPos);
            onBlueprintChanged?.Invoke();
            DisplaceBlock(anchoredPos);
        }

        public void ClearBlueprint()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            ActiveBlueprint.blocks = new BlueprintBlocks();
        }

        public void PlaceBlock(Vector2Int anchoredPos, BlueprintBlock blueprintBlock)
        {
            // Create block
            var block = CreateBlock(blueprintBlock);
            // Keep track of block object by grid anchor position
            blocksByBlueprintAnchor[anchoredPos] = block;
            // Set position of block
            var position = ShipEditorUtils.GetRealPositionByAnchor(blueprintBlock, anchoredPos);
            block.transform.localPosition = position;
        }

        public void DisplaceBlock(Vector2Int anchoredPos)
        {
            Destroy(blocksByBlueprintAnchor[anchoredPos]);
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