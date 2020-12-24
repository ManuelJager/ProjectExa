using Exa.Grids.Blocks.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    [Serializable]
    [CreateAssetMenu(menuName = "Grids/Blocks/Controller")]
    public class ControllerTemplate : BlockTemplate<Controller>
    {
        [SerializeField] public ControllerTemplatePartial controllerTemplatePartial;

        public override IEnumerable<TemplatePartialBase> GetTemplatePartials() {
            return base.GetTemplatePartials()
                .Append(controllerTemplatePartial);
        }
    }
}