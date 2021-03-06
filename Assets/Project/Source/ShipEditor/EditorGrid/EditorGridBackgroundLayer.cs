﻿using Exa.Math;
using Exa.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

#pragma warning disable CS0649

namespace Exa.ShipEditor
{
    public delegate void HoverDelegate(Vector2Int? gridItem);

    public delegate void ExitDelegate();

    public class EditorGridBackgroundLayer : MonoBehaviour
    {
        public event HoverDelegate EnterGrid;

        public event ExitDelegate ExitGrid;

        [SerializeField] private GameObject gridItemPrefab;
        private KeyValuePair<Vector2Int, EditorGridItem>? currActiveGridItem;

        private readonly Dictionary<Vector2Int, EditorGridItem>
            gridItems = new Dictionary<Vector2Int, EditorGridItem>();

        private Vector2Int size = Vector2Int.zero;

        private void OnDisable() {
            if (currActiveGridItem != null) {
                ExitGrid?.Invoke();
                currActiveGridItem = null;
            }
        }

        public void SetGridBackgroundItemColor(Vector2Int? gridPos, bool active) {
            var realGridPos = gridPos.GetValueOrDefault();

            gridItems[realGridPos].SetColor(active);
        }

        public void GenerateGrid(Vector2Int size) {
            if (this.size == size) return;

            this.size = size;

            transform.DestroyChildren();

            gridItems.Clear();

            foreach (var vector in MathUtils.EnumerateVectors(size)) {
                var gridItemGO = Instantiate(gridItemPrefab, transform);
                gridItemGO.transform.localPosition = vector.ToVector3();
                var gridItem = gridItemGO.GetComponent<EditorGridItem>();
                gridItems.Add(vector, gridItem);
            }
        }

        public void UpdateCurrActiveGridItem(Vector2 playerPos) {
            var gridPos = GetGridPosFromMousePos(playerPos);

            // If position isnt in grid
            if (!PosIsInGrid(gridPos)) {
                if (currActiveGridItem != null) {
                    ExitGrid?.Invoke();
                    currActiveGridItem = null;
                }

                return;
            }

            // Get grid item currently hovering over
            var gridItem = gridItems[gridPos];

            // If no currently active grid item has been set
            if (currActiveGridItem == null) {
                currActiveGridItem = new KeyValuePair<Vector2Int, EditorGridItem>(gridPos, gridItem);
                EnterGrid?.Invoke(gridPos);
                return;
            }

            // Get last last frame currently hovering grid item
            var lastGridPos = currActiveGridItem.GetValueOrDefault().Key;
            var lastGridItem = currActiveGridItem?.Value;

            if (lastGridPos == gridPos) return;

            // Grid item events
            EnterGrid?.Invoke(gridPos);
            currActiveGridItem = new KeyValuePair<Vector2Int, EditorGridItem>(gridPos, gridItem);
        }

        public bool PosIsInGrid(Vector2Int pos) {
            return !(
                pos.x >= size.x ||
                pos.y >= size.y ||
                pos.x < 0 ||
                pos.y < 0);
        }

        private Vector2Int GetGridPosFromMousePos(Vector2 playerPos) {
            // Get grid position from world position mouse
            var screenPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            var posXFloor = Mathf.FloorToInt(screenPoint.x - playerPos.x + 0.5f);
            var posYFloor = Mathf.FloorToInt(screenPoint.y - playerPos.y + 0.5f);

            return new Vector2Int(posXFloor, posYFloor);
        }
    }
}