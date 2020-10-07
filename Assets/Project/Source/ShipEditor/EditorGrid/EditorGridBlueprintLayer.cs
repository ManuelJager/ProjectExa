using Exa.Grids.Blueprints;
using Exa.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Exa.ShipEditor
{
    public class EditorGridBlueprintLayer : MonoBehaviour
    {
        public UnityEvent onBlueprintChanged;

        [SerializeField] private ShipEditorStopwatch _stopwatch;
        private Dictionary<Vector2Int, GameObject> _blocksByBlueprintAnchor = new Dictionary<Vector2Int, GameObject>();

        public Blueprint ActiveBlueprint { get; private set; }

        private void OnDisable()
        {
            transform.SetActiveChildren(false);

            _blocksByBlueprintAnchor = new Dictionary<Vector2Int, GameObject>();
        }

        public void Import(Blueprint blueprint)
        {
            ActiveBlueprint = blueprint;

            foreach (var block in blueprint.Blocks)
            {
                PlaceBlock(block);
            }
        }

        public void AddBlock(AnchoredBlueprintBlock anchoredBlueprintBlock)
        {
            // Reset the stopwatch timer used by the shipeditor to time blueprint grid validation
            _stopwatch.Reset();
            onBlueprintChanged?.Invoke();
            PlaceBlock(anchoredBlueprintBlock);
            ActiveBlueprint.Add(anchoredBlueprintBlock);
        }

        public void RemoveBlock(Vector2Int gridPos)
        {
            if (!ActiveBlueprint.Blocks.ContainsMember(gridPos)) return;

            // Reset the stopwatch timer used by the shipeditor to time blueprint grid validation
            _stopwatch.Reset();

            var anchoredBlock = ActiveBlueprint.Blocks.GetMember(gridPos);
            var anchoredPos = anchoredBlock.GridAnchor;

            ActiveBlueprint.Remove(anchoredPos);
            onBlueprintChanged?.Invoke();
            _blocksByBlueprintAnchor[anchoredPos].SetActive(false);
        }

        public void ClearBlueprint()
        {
            transform.SetActiveChildren(false);

            ActiveBlueprint?.ClearBlocks();
        }

        public void PlaceBlock(AnchoredBlueprintBlock block)
        {
            var blockGo = block.CreateInactiveInertBlockInGrid(transform);
            blockGo.SetActive(true);
            _blocksByBlueprintAnchor[block.gridAnchor] = blockGo;
        }
    }
}