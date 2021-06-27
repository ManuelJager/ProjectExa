using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components {
    [Serializable]
    public class ThrusterTemplatePartial : TemplatePartial<ThrusterData> {
        [SerializeField] private float thrust;

        public override ThrusterData ToBaseComponentValues() {
            return new ThrusterData {
                thrust = thrust
            };
        }
    }
}