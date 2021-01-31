using System.Collections;
using System.Collections.Generic;

namespace Exa.Audio.Music
{
    public class CustomSoundTrack : ISoundTrack
    {
        private IEnumerable<ISong> songs;

        public ISoundTrackDescription Description { get; }

        public CustomSoundTrack(IEnumerable<ISong> songs, ISoundTrackDescription description) {
            this.songs = songs;
            Description = description;
        }

        public IEnumerator<ISong> GetEnumerator() {
            return songs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}