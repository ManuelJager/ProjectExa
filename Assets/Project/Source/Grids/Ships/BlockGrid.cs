using Exa.Generics;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using Exa.Grids.Blueprints;
using Exa.Grids.Ships;
using UCommandConsole;
using UnityEngine;

namespace Exa.Grids.Ships
{
    public class BlockGrid : Grid<Block>
    {
        private Transform container;
        private Ship ship;

        public float TotalHull { get; private set; }
        public float CurrentHull { get; set; }
        public CentreOfMassCache CentreOfMass { get; protected set; }

        public BlockGrid(Transform container, Ship ship)
        {
            this.container = container;
            this.ship = ship;
            CentreOfMass = new CentreOfMassCache();
        }

        public override void Add(Block block)
        {
            base.Add(block);

            if (block is IPhysical)
            {
                var localPos = block.anchoredBlueprintBlock.GetLocalPosition();
                var iPhysical = block as IPhysical;
                var mass = iPhysical.PhysicalBehaviour.data.mass;
                CentreOfMass.Add(localPos, mass);
            }
        }

        public override Block Remove(Vector2Int key)
        {
            var block = base.Remove(key);
            
            if (block is IPhysical)
            {
                var localPos = block.anchoredBlueprintBlock.GetLocalPosition();
                CentreOfMass.Remove(localPos);
            }

            return block;
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