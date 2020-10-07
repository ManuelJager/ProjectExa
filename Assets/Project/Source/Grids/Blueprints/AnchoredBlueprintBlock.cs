using Exa.Generics;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Utils;
using Newtonsoft.Json;
using System;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    [Serializable]
    public class AnchoredBlueprintBlock : ICloneable<AnchoredBlueprintBlock>, IGridMember
    {
        public Vector2Int gridAnchor;
        public BlueprintBlock blueprintBlock;

        public Vector2Int GridAnchor
        {
            get => gridAnchor;
            set => gridAnchor = value;
        }

        public BlueprintBlock BlueprintBlock
        {
            get => blueprintBlock;
            set => blueprintBlock = value;
        }

        public AnchoredBlueprintBlock(Vector2Int gridAnchor, BlueprintBlock blueprintBlock)
        {
            this.gridAnchor = gridAnchor;
            this.blueprintBlock = blueprintBlock;
        }

        public GameObject CreateInactiveInertBlockInGrid(Transform parent)
        {
            var blockGo = Systems.Blocks.GetInactiveInertBlock(blueprintBlock.id, parent);
            this.SetupGameObject(blockGo);
            return blockGo;
        }

        public Block CreateInactiveBlockBehaviourInGrid(Transform parent, ShipContext blockPrefabType)
        {
            var block = Systems.Blocks.GetInactiveBlock(blueprintBlock.id, parent, blockPrefabType);
            var blockGo = block.gameObject;
            block.anchoredBlueprintBlock = this;
            this.SetupGameObject(blockGo);
            return block;
        }
        
        public AnchoredBlueprintBlock Clone()
        {
            return new AnchoredBlueprintBlock(gridAnchor, blueprintBlock);
        }

        public void AddGridTotals(GridTotals totals)
        {
            blueprintBlock.Template.AddGridTotals(totals);
        }

        public void RemoveGridTotals(GridTotals totals)
        {
            blueprintBlock.Template.RemoveGridTotals(totals);
        }
    }
}