using Exa.Grids.Blocks;
using UnityEngine;

namespace Exa.Research
{
    public abstract class ResearchItem : ScriptableObject
    {
        public string Id => name;

        public abstract void EnableOn(BlockContext filter);
        public abstract void DisableOn(BlockContext filter);
    }
}