﻿using Exa.Data;
using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class ThrusterTemplatePartial : TemplatePartial<ThrusterData>
    {
        [SerializeField] private float _thrust; 

        public override ThrusterData Convert() => new ThrusterData
        {
            thrust = _thrust
        };
    }
}