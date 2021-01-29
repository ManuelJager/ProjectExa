namespace Exa.Audio.Music
{
    public class DefaultSoundTrackDescription : ISoundTrackDescription
    {
        private ISoundTrack soundTrack;

        public string Name => "Default soundtrack";

        public DefaultSoundTrackDescription(ISoundTrack soundTrack) {
            this.soundTrack = soundTrack;
        }

        public ISoundTrack GetSoundTrack() {
            return soundTrack;
        }
    }
}