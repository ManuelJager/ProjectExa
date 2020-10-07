using Exa.Generics;
using Exa.Grids.Blocks.BlockTypes;
using Exa.UI.Tooltips;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class PhysicalTemplatePartial : TemplatePartial<PhysicalData>
    {
        [SerializeField] private float _maxHull;
        [SerializeField] private float _armor;
        [SerializeField] private float _mass; // In ton

        public float MaxHull => _maxHull;
        public float Armor => _armor;
        public float Mass => _mass;

        public override PhysicalData Convert() => new PhysicalData
        {
            armor = _armor,
            hull = _maxHull,
            mass = _mass
        };
    }
}