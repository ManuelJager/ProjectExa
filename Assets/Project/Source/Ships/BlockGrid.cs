using Exa.Grids;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blueprints;
using Exa.Utils;
using System.Text;
using UnityEngine;

namespace Exa.Ships
{
    public class BlockGrid : Grid<Block>
    {
        private Transform container;
        private Ship ship;

        public CentreOfMassCache CentreOfMass { get; protected set; }

        // As this is a special member, we keep a separate reference
        public Block Controller { get; private set; }

        public BlockGrid(Transform container, Ship ship)
        {
            this.container = container;
            this.ship = ship;
            CentreOfMass = new CentreOfMassCache();
        }

        public override void Add(Block gridMember)
        {
            if (gridMember.BlueprintBlock.Template.category == BlockCategory.Controller)
            {
                if (Controller != null)
                {
                    throw new DuplicateControllerException(gridMember.GridAnchor);
                }

                Controller = gridMember;
            }
            base.Add(gridMember);
        }

        public override Block Remove(Vector2Int key)
        {
            var block = base.Remove(key);
            if (ReferenceEquals(block, Controller))
            {
                Controller = null;
            }
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