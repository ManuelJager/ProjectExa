using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components {
    [Serializable]
    public class ShieldGeneratorTemplatePartial : TemplatePartial<ShieldGeneratorData> {
        [SerializeField] private ShieldGeneratorData data;
        
        public override ShieldGeneratorData ToBaseComponentValues() {
            return data;
        }
    }
}