using System;

namespace Exa.AI
{
    [Flags]
    public enum ActionLane : uint
    {
        Movement    = 1 << 0,
        Target      = 1 << 1,
    }
}