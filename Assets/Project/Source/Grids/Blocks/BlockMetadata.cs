using System;
using Exa.UI.Tooltips;
using UnityEngine;

namespace Exa.Grids.Blocks
{
    [Serializable]
    public struct BlockMetadata : ITooltipComponent
    {
        public int strength;
        public int creditCost;

        public static BlockMetadata operator +(BlockMetadata a, BlockMetadata b) {
            a.strength += b.strength;
            a.creditCost += b.creditCost;

            return a;
        }

        public static BlockMetadata operator -(BlockMetadata a, BlockMetadata b) {
            a.strength -= b.strength;
            a.creditCost -= b.creditCost;

            return a;
        }

        public TooltipComponentView InstantiateComponentView(Transform parent) {
            return Systems.UI.Tooltips.tooltipGenerator.CreateMetadataView(parent, this);
        }
    }
}