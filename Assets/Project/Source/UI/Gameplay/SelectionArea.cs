using UnityEngine;

namespace Exa.UI.Gameplay
{
    public class SelectionArea : MonoBehaviour
    {
        [SerializeField] private RectTransform _rect;
        private Vector2 _startWorldPoint;
        private Vector2 _endWorldPoint;

        private void Awake()
        {
            _startWorldPoint = Vector2.zero;
            _endWorldPoint = Vector2.zero;
        }

        private void Update()
        {
            SetPosition();
        }

        public void Show(Vector2 startWorldPoint)
        {
            _rect.gameObject.SetActive(true);
            this._startWorldPoint = startWorldPoint;
            SetEnd(startWorldPoint);
        }

        public void SetEnd(Vector2 endWorldPoint)
        {
            this._endWorldPoint = endWorldPoint;
            SetPosition();
        }

        public void Hide()
        {
            _rect.gameObject.SetActive(false);
        }

        private void SetPosition()
        {
            var start = GetScreenSpace(_startWorldPoint);
            var end = GetScreenSpace(_endWorldPoint);

            var min = new Vector2
            {
                x = Mathf.Min(start.x, end.x),
                y = Mathf.Min(start.y, end.y)
            };

            var max = new Vector2
            {
                x = Mathf.Max(start.x, end.x),
                y = Mathf.Max(start.y, end.y)
            };

            _rect.anchoredPosition = min;
            _rect.sizeDelta = max - min;
        }

        private Vector2 GetScreenSpace(Vector2 worldPos)
        {
            return Camera.main.WorldToScreenPoint(worldPos);
        }
    }
}