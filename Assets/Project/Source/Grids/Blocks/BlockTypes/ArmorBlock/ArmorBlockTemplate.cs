using Exa.Grids.Blocks.Components;
using System;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    [Serializable]
    [CreateAssetMenu(fileName = "Armor", menuName = "Grids/Blocks/Armor")]
    public class ArmorBlockTemplate : BlockTemplate<ArmorBlock>
    {
        public PhysicalBlockTemplateComponent templateComponent;

        public override void SetValues(ArmorBlock block)
        {
            block.physicalBlockData = templateComponent.Convert();
        }
    }
}