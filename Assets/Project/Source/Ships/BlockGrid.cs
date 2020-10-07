using Exa.Grids;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blueprints;
using Exa.UI.Tooltips;
using Exa.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Exa.Ships
{
    public class BlockGrid : Grid<Block>
    {
        private readonly Transform container;
        private readonly Ship ship;

        public CentreOfMassCache CentreOfMass { get; protected set; }

        public BlockGrid(Transform container, Ship ship)
            : base(totals: ship.Totals)
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

        internal void Import(Blueprint blueprint, ShipContext blockContext)
        {
            foreach (var anchoredBlueprintBlock in blueprint.Blocks)
            {
                Add(CreateBlock(anchoredBlueprintBlock, blockContext));
            }
        }

        private Block CreateBlock(AnchoredBlueprintBlock anchoredBlueprintBlock, ShipContext blockContext)
        {
            var block = anchoredBlueprintBlock.CreateInactiveBlockBehaviourInGrid(container, blockContext);
            block.Ship = ship;
            block.gameObject.SetActive(true);
            return block;
        }

        public IEnumerable<ITooltipComponent> GetDebugTooltipComponents() => new ITooltipComponent[]
        {
            new TooltipText(CentreOfMass.ToString())
        };
    }
}