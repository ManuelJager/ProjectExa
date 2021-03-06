﻿using Exa.Pooling;

namespace Exa.Grids.Blocks
{
    public enum PrefabType
    {
        inert,
        alive
    }

    public class InertBlockPoolGroup : BlockPoolGroupBase
    {
        protected override PrefabType PrefabType => PrefabType.inert;

        /// <summary>
        /// Creates an inert block prefab on this group
        /// </summary> 
        /// <param name="blockTemplate"></param>
        /// <returns></returns>
        public void CreateInertPrefab(BlockTemplate blockTemplate) {
            var id = blockTemplate.id;
            var prefab = CreatePrefab(blockTemplate, PrefabType);
            var pool = CreatePool<Pool>(prefab, $"Inert block pool: {id}", out var settings);
            poolById[id] = pool;
            pool.Configure(settings);
        }
    }
}