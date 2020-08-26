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

        public BlockGrid(Transform container, Ship ship)
            : base(totals: new ShipGridTotals(ship.rigidbody))
        {
            this.container = container;
            this.ship = ship;
            CentreOfMass = new CentreOfMassCache();
        }

        public override void Add(Block gridMember)
        {
            var isController = gridMember.BlueprintBlock.Template.category.Is(BlockCategory.Controller);
            if (isController && Controller != null)
            {
                throw new DuplicateControllerException(gridMember.GridAnchor);
            }
            base.Add(gridMember);
        }

        internal void Import(Blueprint blueprint, BlockContext blockContext)
        {
            foreach (var anchoredBlueprintBlock in blueprint.Blocks)
            {
                Add(CreateBlock(anchoredBlueprintBlock, blockContext));
            }
        }

        private Block CreateBlock(AnchoredBlueprintBlock anchoredBlueprintBlock, BlockContext blockContext)
        {
            var block = anchoredBlueprintBlock.CreateInactiveBlockBehaviourInGrid(container, blockContext);
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