using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using Exa.Grids.Blueprints;
using Exa.Ships;
using Exa.Utils;
using System.Text;
using UnityEngine;

namespace Exa.Grids.Ships
{
    public class BlockGrid : Grid<Block>
    {
        private Transform container;
        private Ship ship;

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

            var localPos = block.anchoredBlueprintBlock.GetLocalPosition();
            var mass = block.PhysicalBehaviour.Data.mass;
            CentreOfMass.Add(localPos, mass);
        }

        public override Block Remove(Vector2Int key)
        {
            var block = base.Remove(key);

            var localPos = block.anchoredBlueprintBlock.GetLocalPosition();
            CentreOfMass.Remove(localPos);

            return block;
        }

        internal void Import(Blueprint blueprint)
        {
            foreach (var anchoredBlueprintBlock in blueprint.Blocks)
            {
                Add(CreateBlock(anchoredBlueprintBlock));
            }
        }

        private Block CreateBlock(AnchoredBlueprintBlock anchoredBlueprintBlock)
        {
            var block = anchoredBlueprintBlock.CreateInactiveBlockBehaviourInGrid(container, BlockPrefabType.userGroup);
            block.Ship = ship;
            block.gameObject.SetActive(true);
            anchoredBlueprintBlock.blueprintBlock.RuntimeContext.SetValues(block);
            return block;
        }

        public string ToString(int tabs = 0)
        {
            var sb = new StringBuilder();
            sb.AppendLineIndented(CentreOfMass.ToString(), tabs);
            return sb.ToString();
        }
    }
}