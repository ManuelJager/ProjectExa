using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using Exa.Grids.Blueprints;
using Exa.Grids.Ships;
using UnityEngine;

namespace Exa.Grids
{
    public class BlockGrid : Grid<Block>
    {
        private Transform container;
        private Ship ship;

        public float TotalHull { get; private set; }
        public float CurrentHull { get; set; }

        public BlockGrid(Transform container, Ship ship)
        {
            this.container = container;
            this.ship = ship;
        }

        internal void Import(Blueprint blueprint)
        {
            foreach (var anchoredBlueprintBlock in blueprint.Blocks)
            {
                var context = anchoredBlueprintBlock.blueprintBlock.RuntimeContext;

                if (context is IPhysicalTemplatePartial)
                {
                    var partial = context as IPhysicalTemplatePartial;
                    var maxHull = partial.PhysicalTemplatePartial.MaxHull;
                    TotalHull += maxHull;
                }

                Add(CreateBlock(anchoredBlueprintBlock));
            }
        }

        private Block CreateBlock(AnchoredBlueprintBlock anchoredBlueprintBlock)
        {
            var block = anchoredBlueprintBlock.CreateInactiveBlockBehaviourInGrid(container, BlockPrefabType.userGroup);
            block.Ship = ship;
            block.gameObject.SetActive(true);
            return block;
        }
    }
}