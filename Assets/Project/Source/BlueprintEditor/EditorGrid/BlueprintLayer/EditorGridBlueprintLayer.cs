﻿using System.Collections;
using Exa.Grids.Blueprints;
using Exa.Utils;
using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks.BlockTypes;
using UnityEngine;
using UnityEngine.Events;

#pragma warning disable CS0649

namespace Exa.ShipEditor
{
    public class EditorGridBlueprintLayer : MonoBehaviour
    {
        public UnityEvent onBlueprintChanged;
        [SerializeField] private GridEditorStopwatch stopwatch;
        [SerializeField] private EditorGrid editorGrid;

        private Dictionary<Vector2Int, GameObject> blocksByBlueprintAnchor = new Dictionary<Vector2Int, GameObject>();

        public Blueprint ActiveBlueprint { get; private set; }

        private void OnDisable() {
            transform.SetActiveChildren(false);

            blocksByBlueprintAnchor = new Dictionary<Vector2Int, GameObject>();
        }

        public IEnumerator Import(Blueprint blueprint) {
            ActiveBlueprint = blueprint;

            foreach (var block in blueprint.Blocks) {
                yield return new WaitForSeconds(0.05f);
                PlaceBlock(block);
            }
        }

        public void AddBlock(ABpBlock block) {
            // Reset the stopwatch timer used by the shipeditor to time blueprint grid validation
            stopwatch.Reset();
            onBlueprintChanged?.Invoke();
            PlaceBlock(block);
            ActiveBlueprint.Add(block);
        }

        public void PlaceBlock(ABpBlock block) {
            var blockGO = block.CreateInactiveInertBlockInGrid(transform);
            blockGO.SetActive(true);
            blocksByBlueprintAnchor[block.gridAnchor] = blockGO;

            editorGrid.CustomLayers.ForEach(layer => layer.TryAddToGrid(block));
        }

        public void RemoveBlock(Vector2Int gridPos) {
            if (!ActiveBlueprint.Blocks.ContainsMember(gridPos)) return;

            RemoveBlock(ActiveBlueprint.Blocks.GetMember(gridPos));
        }

        public void RemoveBlock(ABpBlock block) {
            // Reset the stopwatch timer used by the shipeditor to time blueprint grid validation
            stopwatch.Reset();
            var pos = block.GridAnchor;

            ActiveBlueprint.Remove(pos);
            onBlueprintChanged?.Invoke();

            editorGrid.CustomLayers.ForEach(layer => layer.TryRemoveFromGrid(block));

            blocksByBlueprintAnchor[pos].SetActive(false);
        }

        public void ClearBlueprint() {
            transform.SetActiveChildren(false);

            ActiveBlueprint?.ClearBlocks();
        }
    }
}