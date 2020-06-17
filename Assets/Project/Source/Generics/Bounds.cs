using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Generics
{
    public struct Bounds
    {
        public MinMax<Vector2Int> minMax;

        public Bounds(MinMax<Vector2Int> minMax)
        {
            this.minMax = minMax;
        }

        public Bounds(IEnumerable<Vector2Int> positions)
        {
            minMax = new MinMax<Vector2Int>();

            var first = positions.First();
            minMax.min = first;
            minMax.max = first;

            foreach (var position in positions)
            {
                if (position.x < minMax.min.x) minMax.min.x = position.x;
                if (position.y < minMax.min.y) minMax.min.y = position.y;
                if (position.x > minMax.max.x) minMax.max.x = position.x;
                if (position.y > minMax.max.y) minMax.max.y = position.y;
            }
        }

        public IEnumerable<Vector2Int> GetAdjacentPositions()
        {
            // Get bottom and top adjacent positions
            for (int i = minMax.min.x; i < minMax.max.x + 1; i++)
            {
                yield return new Vector2Int(i, minMax.min.y - 1);
                yield return new Vector2Int(i, minMax.max.y + 1);
            }

            // Get right and left adjacent position
            for (int i = minMax.min.y; i < minMax.max.y + 1; i++)
            {
                yield return new Vector2Int(minMax.min.x - 1, i);
                yield return new Vector2Int(minMax.max.x + 1, i);
            }
        }
    }
}