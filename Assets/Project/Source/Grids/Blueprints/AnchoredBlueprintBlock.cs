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

        public GameObject CreateInertBehaviourInGrid(Transform parent)
        {
            var blockGO = Systems.BlockFactory.GetInertBlock(blueprintBlock.id, parent);
            this.SetupGameObject(blockGO);
            return blockGO;
        }

        public Block CreateBehaviourInGrid(Transform parent, BlockPrefabType blockPrefabType)
        {
            var block = Systems.BlockFactory.GetBlock(blueprintBlock.id, parent, blockPrefabType);
            block.anchoredBlueprintBlock = this;
            this.SetupGameObject(block.gameObject);
            return block;
        }
        
        public AnchoredBlueprintBlock Clone()
        {
            return new AnchoredBlueprintBlock(gridAnchor, blueprintBlock);
        }
    }
}