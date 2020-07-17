using Exa.Grids.Blueprints;
using Unity.Entities;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    /// <summary>
    /// Base class for blocks
    /// </summary>
    public class Block : MonoBehaviour, IBlock, IConvertGameObjectToEntity
    {
        public AnchoredBlueprintBlock anchoredBlueprintBlock;
        public ShipGrid shipGrid;

        // TODO: Convert gameobject to entity
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            throw new System.NotImplementedException();
        }
    }
}