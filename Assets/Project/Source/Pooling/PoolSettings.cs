﻿using Exa.Generics;
using System;
using UnityEngine;

namespace Exa.Pooling
{
    [Serializable]
    public class PoolSettings : ICloneable<PoolSettings>
    {
        public int maxSize;
        public int preferredSize;
        public bool growToPreferredSize;
        public GameObject prefab;

        public PoolSettings Clone() => new PoolSettings {
            maxSize = maxSize,
            preferredSize = preferredSize,
            growToPreferredSize = growToPreferredSize,
            prefab = prefab
        };
    }
}