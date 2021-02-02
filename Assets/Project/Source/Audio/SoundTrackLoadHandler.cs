using System;
using System.Collections;
using Exa.Audio.Music;

namespace Exa.Audio
{
    public class SoundTrackLoadHandler
    {
        public IProgress<float> Reporter { get; set; }
        public IEnumerator LoadEnumerator { get; set; }
        public ISoundTrack OutputSoundtrack { get; set; }
    }
}