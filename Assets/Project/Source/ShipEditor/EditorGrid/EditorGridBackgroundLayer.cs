using Exa.Math;
using Exa.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Exa.ShipEditor
{
    public delegate void HoverDelegate(Vector2Int? gridItem);

    public delegate void ExitDelegate();

    public class EditorGridBackgroundLayer : MonoBehaviour
    {
        public event HoverDelegate EnterGrid;

        public event ExitDelegate ExitGrid;

        [SerializeField] private GameObject _gridItemPrefab;
        private KeyValuePair<Vector2Int, EditorGridItem>? _currActiveGridItem;
        private readonly Dictionary<Vector2Int, EditorGridItem> _gridItems = new Dictionary<Vector2Int, EditorGridItem>();
        private Vector2Int _size = Vector2Int.zero;

        private void OnDisable()
        {
            if (_currActiveGridItem != null)
            {
                ExitGrid?.Invoke();
                _currActiveGridItem = null;
            }
        }

        public void SetGridBackgroundItemColor(Vector2Int? gridPos, bool active)
        {
            var realGridPos = gridPos.GetValueOrDefault();

            _gridItems[realGridPos].SetColor(active);
        }

        public void GenerateGrid(Vector2Int size)
        {
            if (this._size == size) return;

            this._size = size;

            transform.DestroyChildren();

            _gridItems.Clear();

            foreach (var vector in MathUtils.EnumerateVectors(size))
            {
                var gridItemGo = Instantiate(_gridItemPrefab, transform);
                gridItemGo.transform.localPosition = vector.ToVector3();
                var gridItem = gridItemGo.GetComponent<EditorGridItem>();
                _gridItems.Add(vector, gridItem);
            }
        }

        public void UpdateCurrActiveGridItem(Vector2 playerPos)
        {
            var gridPos = GetGridPosFromMousePos(playerPos);

            // If position isnt in grid
            if (!PosIsInGrid(gridPos))
            {
                if (_currActiveGridItem != null)
                {
                    ExitGrid?.Invoke();
                    _currActiveGridItem = null;
                }
                return;
            }

            // Get grid item currently hovering over
            var gridItem = _gridItems[gridPos];

            // If no currently active grid item has been set
            if (_currActiveGridItem == null)
            {
                _currActiveGridItem = new KeyValuePair<Vector2Int, EditorGridItem>(gridPos, gridItem);
                EnterGrid?.Invoke(gridPos);
                return;
            }

            // Get last last frame currently hovering grid item
            var lastGridPos = _currActiveGridItem.GetValueOrDefault().Key;
            var lastGridItem = _currActiveGridItem?.Value;

            if (lastGridPos == gridPos) return;

            // Grid item events
            EnterGrid?.Invoke(gridPos);
            _currActiveGridItem = new KeyValuePair<Vector2Int, EditorGridItem>(gridPos, gridItem);
        }

        public bool PosIsInGrid(Vector2Int pos)
        {
            return !(
                pos.x >= _size.x ||
                pos.y >= _size.y ||
                pos.x < 0 ||
                pos.y < 0);
        }

        private Vector2Int GetGridPosFromMousePos(Vector2 playerPos)
        {
            // Get grid position from world position mouse
            var screenPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            var posXFloor = Mathf.FloorToInt(screenPoint.x - playerPos.x + 0.5f);
            var posYFloor = Mathf.FloorToInt(screenPoint.y - playerPos.y + 0.5f);

            return new Vector2Int(posXFloor, posYFloor);
        }
    }
}