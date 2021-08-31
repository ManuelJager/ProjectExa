using System;

namespace Exa.Audio {
    [Flags]
    public enum AudioType {
        Soundtrack = 1 << 0,
        InterfaceSFX = 1 << 1,
        GameplaySFX = 1 << 2,
    }
}