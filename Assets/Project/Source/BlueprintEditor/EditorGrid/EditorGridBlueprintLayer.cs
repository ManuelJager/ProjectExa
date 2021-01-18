﻿using Exa.Grids.Blueprints;
using Exa.Utils;
using System.Collections.Generic;
using Exa.Grids.Blocks.BlockTypes;
using UnityEngine;
using UnityEngine.Events;

#pragma warning disable CS0649

namespace Exa.ShipEditor
{
    public class EditorGridBlueprintLayer : MonoBehaviour
    {
        public UnityEvent onBlueprintChanged;
        [SerializeField] private EditorGridTurretLayer turretLayer;
        [SerializeField] private GridEditorStopwatch stopwatch;

        private Dictionary<Vector2Int, GameObject> blocksByBlueprintAnchor = new Dictionary<Vector2Int, GameObject>();

        public Blueprint ActiveBlueprint { get; private set; }

        private void OnDisable() {
            transform.SetActiveChildren(false);

            blocksByBlueprintAnchor = new Dictionary<Vector2Int, GameObject>();
        }

        public void Import(Blueprint blueprint) {
            ActiveBlueprint = blueprint;

            foreach (var block in blueprint.Blocks) {
                PlaceBlock(block);
            }
        }

        public void AddBlock(AnchoredBlueprintBlock block) {
            // Reset the stopwatch timer used by the shipeditor to time blueprint grid validation
            stopwatch.Reset();
            onBlueprintChanged?.Invoke();
            PlaceBlock(block);
            ActiveBlueprint.Add(block);
        }

        public void RemoveBlock(Vector2Int gridPos) {
            if (!ActiveBlueprint.Blocks.ContainsMember(gridPos)) return;

            // Reset the stopwatch timer used by the shipeditor to time blueprint grid validation
            stopwatch.Reset();

            var block = ActiveBlueprint.Blocks.GetMember(gridPos);
            var pos = block.GridAnchor;

            ActiveBlueprint.Remove(pos);
            onBlueprintChanged?.Invoke();
            blocksByBlueprintAnchor[pos].SetActive(false);

            if (block.blueprintBlock.Template is ITurretTemplate) {
                turretLayer.RemoveStationaryOverlay(block);
            }
        }

        public void ClearBlueprint() {
            transform.SetActiveChildren(false);

            ActiveBlueprint?.ClearBlocks();
        }

        public void PlaceBlock(AnchoredBlueprintBlock block) {
            var blockGO = block.CreateInactiveInertBlockInGrid(transform);
            blockGO.SetActive(true);
            blocksByBlueprintAnchor[block.gridAnchor] = blockGO;
            
            if (block.blueprintBlock.Template is ITurretTemplate template) {
                turretLayer.AddStationaryOverlay(block, template);
            }
        }
    }
}