using Unity.Entities.UniversalDelegates;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Components
{
    public class FlexibleLayoutGroup : LayoutGroup
    {
        public int rows;
        public int columns;
        public Vector2 cellSize;

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();

            var sqrRt = Mathf.Sqrt(transform.childCount);
            rows = Mathf.CeilToInt(sqrRt);
            columns = Mathf.CeilToInt(sqrRt);

            var parentWidth = rectTransform.rect.width;
            var parentHeight = rectTransform.rect.height;

            var cellWidth = parentWidth / (float)columns;
            var cellHeight = parentHeight / (float)rows;

            cellSize.x = cellWidth;
            cellSize.y = cellHeight;

            var columnCount = 0;
            var rowCount = 0;

            for (int i = 0; i < rectChildren.Count; i++)
            {
                rowCount = i / columns;
                columnCount = i % columns;

                var item = rectChildren[i];

                var xPos = (cellSize.x * columnCount);
                var yPos = (cellSize.y * rowCount);

                SetChildAlongAxis(item, 0, xPos, cellSize.x);
                SetChildAlongAxis(item, 1, yPos, cellSize.y);
            }
        }

        public override void CalculateLayoutInputVertical()
        {
        }

        public override void SetLayoutHorizontal()
        {
        }

        public override void SetLayoutVertical()
        {
        }
    }
}