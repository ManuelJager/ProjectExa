﻿using Exa.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Exa.Grids.Blueprints.Editor
{
    public delegate void HoverDelegate(Vector2Int? gridItem);

    public delegate void ExitDelegate();

    public class EditorGridBackgroundLayer : MonoBehaviour
    {
        public event HoverDelegate EnterGrid;

        public event ExitDelegate ExitGrid;

        [SerializeField] private GameObject gridItemPrefab;
        private KeyValuePair<Vector2Int, EditorGridItem>? currActiveGridItem;
        private Dictionary<Vector2Int, EditorGridItem> gridItems = new Dictionary<Vector2Int, EditorGridItem>();
        private Vector2Int size = Vector2Int.zero;

        private void OnDisable()
        {
            if (currActiveGridItem != null)
            {
                ExitGrid?.Invoke();
                currActiveGridItem = null;
            }
        }

        public void SetGridBackgroundItemColor(Vector2Int? gridPos, bool active)
        {
            var realGridPos = gridPos.GetValueOrDefault();

            gridItems[realGridPos].SetColor(active);
        }

        public void GenerateGrid(Vector2Int size)
        {
            if (this.size == size) return;

            this.size = size;

            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            gridItems.Clear();

            foreach (var vector in VectorHelpers.EnumerateVectors(size))
            {
                var gridItem = Instantiate(gridItemPrefab, transform);
                gridItem.transform.localPosition = new Vector3(vector.x + 0.5f, vector.y + 0.5f);
                gridItems.Add(vector, gridItem.GetComponent<EditorGridItem>());
            }
        }

        public void UpdateCurrActiveGridItem(Vector2 playerPos)
        {
            var gridPos = GetGridPosFromMousePos(playerPos);

            // If position isnt in grid
            if (!PosIsInGrid(gridPos))
            {
                if (currActiveGridItem != null)
                {
                    ExitGrid?.Invoke();
                    currActiveGridItem = null;
                }
                return;
            }

            // Get grid item currently hovering over
            var gridItem = gridItems[gridPos];

            // If no currently active grid item has been set
            if (currActiveGridItem == null)
            {
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

        public bool PosIsInGrid(Vector2Int pos)
        {
            return !(
                pos.x >= size.x ||
                pos.y >= size.y ||
                pos.x < 0 ||
                pos.y < 0);
        }

        private Vector2Int GetGridPosFromMousePos(Vector2 playerPos)
        {
            // Get grid position from world position mouse
            var worldMousePosv3 = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            var worldMousePosv2 = new Vector2(worldMousePosv3.x, worldMousePosv3.y);

            var posXFloor = Mathf.FloorToInt(worldMousePosv2.x - playerPos.x);
            var posYFloor = Mathf.FloorToInt(worldMousePosv2.y - playerPos.y);

            return new Vector2Int(posXFloor, posYFloor);
        }
    }
}