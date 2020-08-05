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
            var blockGO = Systems.BlockFactory.GetInactiveInertBlock(blueprintBlock.id, parent);
            this.SetupGameObject(blockGO);
            return blockGO;
        }

        public Block CreateInactiveBlockBehaviourInGrid(Transform parent, BlockPrefabType blockPrefabType)
        {
            var block = Systems.BlockFactory.GetInactiveBlock(blueprintBlock.id, parent, blockPrefabType);
            var blockGO = block.gameObject;
            block.anchoredBlueprintBlock = this;
            this.SetupGameObject(blockGO);
            return block;
        }
        
        public AnchoredBlueprintBlock Clone()
        {
            return new AnchoredBlueprintBlock(gridAnchor, blueprintBlock);
        }
    }
}