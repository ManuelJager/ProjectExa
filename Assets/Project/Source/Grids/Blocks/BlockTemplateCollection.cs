using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blocks
{
    /// <summary>
    /// Owner object for all game blocks
    /// </summary>
    [CreateAssetMenu(fileName = "BlockTemplateCollection", menuName = "Grids/Blocks/BlockTemplateCollection")]
    public class BlockTemplateCollection : ScriptableObject
    {
        public List<BlockTemplate> blocks;
    }
}