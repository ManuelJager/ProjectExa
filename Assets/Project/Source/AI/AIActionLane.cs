using System;

namespace Exa.AI
{
    [Flags]
    public enum AIActionLane : uint
    {
        Movement    = 1 << 0,
        Target      = 1 << 1,
    }
}