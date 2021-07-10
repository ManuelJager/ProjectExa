using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks.Components;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes {
    [Serializable]
    [CreateAssetMenu(menuName = "Grids/Blocks/ShipController")]
    public class ShipControllerTemplate : BlockTemplate<ShipController> {
        [SerializeField] private GenericTemplatePartial<ShipControllerData> shipControllerPartial;

        public override IEnumerable<TemplatePartialBase> GetTemplatePartials() {
            return base.GetTemplatePartials().Append(shipControllerPartial);
        }
    }
}