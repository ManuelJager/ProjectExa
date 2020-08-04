using Exa.Grids.Blocks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Exa.Grids.Blueprints.Editor
{
    public class EditorGridBlueprintLayer : MonoBehaviour
    {
        public UnityEvent onBlueprintChanged;

        [SerializeField] private ShipEditorStopwatch stopwatch;
        private Dictionary<Vector2Int, GameObject> blocksByBlueprintAnchor = new Dictionary<Vector2Int, GameObject>();

        public Blueprint ActiveBlueprint { get; private set; }

        public void Import(Blueprint blueprint)
        {
            ClearBlueprint();

            ActiveBlueprint = blueprint;
            blocksByBlueprintAnchor = new Dictionary<Vector2Int, GameObject>();

            foreach (var block in blueprint.Blocks)
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
            if (!ActiveBlueprint.Blocks.ContainsMember(gridPos)) return;

            // Reset the stopwatch timer used by the shipeditor to time blueprint grid validation
            stopwatch.Reset();

            var anchoredBlock = ActiveBlueprint.Blocks.GetMember(gridPos);
            var anchoredPos = anchoredBlock.GridAnchor;

            ActiveBlueprint.Remove(anchoredPos);
            onBlueprintChanged?.Invoke();
            blocksByBlueprintAnchor[anchoredPos].SetActive(false);
        }

        public void ClearBlueprint()
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }

            ActiveBlueprint?.ClearBlocks();
        }

        public void PlaceBlock(AnchoredBlueprintBlock block)
        {
            var blockGO = block.CreateInertBehaviourInGrid(transform);
            blockGO.SetActive(true);
            blocksByBlueprintAnchor[block.gridAnchor] = blockGO;
        }
    }
}