using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blocks
{
    [CreateAssetMenu(fileName = "BlockTemplateCollection", menuName = "Grids/Blocks/BlockTemplateCollection")]
    public class BlockTemplateCollection : ScriptableObject
    {
        public List<BlockTemplate> blocks;
    }
}