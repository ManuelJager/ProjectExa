using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks.Components;
using UnityEngine;

namespace Exa.Grids.Blocks {
    [Serializable]
    [CreateAssetMenu(menuName = "Grids/Blocks/DroneBay")]
    public class DroneBayTemplate : BlockTemplate {
       [SerializeField] private TemplatePartial<DroneBayData> droneBayPartial;
   
       public override IEnumerable<TemplatePartialBase> GetTemplatePartials() {
           return base.GetTemplatePartials().Append(droneBayPartial);
       }
    }
}
