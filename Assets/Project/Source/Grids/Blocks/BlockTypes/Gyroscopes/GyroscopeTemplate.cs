using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks.Components;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes {
    [Serializable]
    [CreateAssetMenu(menuName = "Grids/Blocks/Gyroscope")]
    public class GyroscopeTemplate : BlockTemplate<Gyroscope> {
        [SerializeField] protected GyroscopeTemplatePartial gyroscopeTemplatePartial;

        public override IEnumerable<TemplatePartialBase> GetTemplatePartials() {
            return base.GetTemplatePartials()
                .Append(gyroscopeTemplatePartial);
        }
    }
}