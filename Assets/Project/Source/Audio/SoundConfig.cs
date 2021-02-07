using System;

namespace Exa.Audio
{
    [Serializable]
    public class SoundConfig
    {
        public float volume = 1f;
        public float pitch = 1f;
        public bool allowMultipleOfType = false;
        public bool allowMultipleOnTrack = true;
    }
}