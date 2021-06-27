using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components {
    [Serializable]
    public class GyroscopeTemplatePartial : TemplatePartial<GyroscopeData> {
        [SerializeField] private float turningPower;

        public override GyroscopeData ToBaseComponentValues() {
            return new GyroscopeData {
                turningPower = turningPower
            };
        }
    }
}