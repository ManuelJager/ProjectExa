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

        public void LoadSoundTrack(SoundTrackLoadHandler loadHandler) {
            loadHandler.LoadEnumerator = Load(loadHandler);
        }

        private IEnumerator Load(SoundTrackLoadHandler loadHandler) {
            yield return null;

            loadHandler.Reporter.Report(1);
            loadHandler.OutputSoundtrack = soundTrack;
        }
    }
}