using System;
using Exa.UI.Tooltips;
using UnityEngine;

namespace Exa.Grids.Blocks {
    [Serializable]
    public struct BlockMetadata {
        public int strength;
        public BlockCosts blockCosts;

        public static BlockMetadata operator +(BlockMetadata a, BlockMetadata b) {
            return new BlockMetadata {
                strength = a.strength + b.strength,
                blockCosts = a.blockCosts + b.blockCosts
            };
        }

        public static BlockMetadata operator -(BlockMetadata a, BlockMetadata b) {
            return new BlockMetadata {
                strength = a.strength - b.strength,
                blockCosts = a.blockCosts - b.blockCosts
            };
        }
    }

    [Serializable]
    public struct BlockCosts : ITooltipComponent {
        public int creditCost;
        public int metalsCost;

        public TooltipComponentView InstantiateComponentView(Transform parent) {
            return Systems.UI.Tooltips.tooltipGenerator.CreateBlockCostsView(parent, this);
        }

        public static BlockCosts operator +(BlockCosts a, BlockCosts b) {
            return new BlockCosts {
                creditCost = a.creditCost + b.creditCost,
                metalsCost = a.metalsCost + b.metalsCost
            };
        }

        public static BlockCosts operator -(BlockCosts a, BlockCosts b) {
            return new BlockCosts {
                creditCost = a.creditCost - b.creditCost,
                metalsCost = a.metalsCost - b.metalsCost
            };
        }

        public static BlockCosts operator *(BlockCosts a, float b) {
            return new BlockCosts {
                creditCost = Mathf.RoundToInt(a.creditCost * b),
                metalsCost = Mathf.RoundToInt(a.metalsCost * b)
            };
        }
    }
}