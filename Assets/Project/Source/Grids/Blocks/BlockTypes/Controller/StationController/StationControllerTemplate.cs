using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks.Components;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes {
    [Serializable]
    [CreateAssetMenu(menuName = "Grids/Blocks/ShipController")]
    public abstract class StationControllerTemplate<T> : BlockTemplate<T>
        where T : Block {
        [SerializeField] private TemplatePartial<StationControllerData> stationControllerPartial;

        public override IEnumerable<TemplatePartialBase> GetTemplatePartials() {
            return base.GetTemplatePartials().Append(stationControllerPartial);
        }
    }
}