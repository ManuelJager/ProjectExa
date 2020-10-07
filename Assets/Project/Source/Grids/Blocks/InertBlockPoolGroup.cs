using Exa.Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blocks
{
    public enum PrefabType
    {
        Inert,
        Alive
    }

    public class InertBlockPoolGroup : BlockPoolGroupBase
    {
        protected override PrefabType PrefabType => PrefabType.Inert; 

        /// <summary>
        /// Creates an inert block prefab on this group
        /// </summary> 
        /// <param name="blockTemplate"></param>
        /// <returns></returns>
        public void CreateInertPrefab(BlockTemplate blockTemplate)
        {
            var id = blockTemplate.id;
            var prefab = CreatePrefab(blockTemplate, PrefabType);
            var pool = CreatePool<Pool>(prefab, $"Inert block pool: {id}", out var settings);
            poolById[id] = pool;
            pool.Configure(settings);
        }
    }
}