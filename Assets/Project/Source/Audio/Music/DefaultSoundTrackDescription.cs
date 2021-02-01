using System;
using System.Collections;
using System.Linq;

namespace Exa.Audio.Music
{
    public class DefaultSoundTrackDescription : ISoundTrackDescription
    {
        private ISoundTrack soundTrack;

        public string Name => "Default soundtrack";
        public int SongCount => soundTrack.Count();

        public DefaultSoundTrackDescription(ISoundTrack soundTrack) {
            this.soundTrack = soundTrack;
        }

        public ISoundTrack GetSoundTrack(IProgress<float> progress, out IEnumerator loadEnumerator) {
            loadEnumerator = null;
            return soundTrack;
        }
    }
}