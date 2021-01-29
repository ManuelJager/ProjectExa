namespace Exa.Audio.Music
{
    public interface ISoundTrackDescription
    {
        public string Name { get; }
        public ISoundTrack GetSoundTrack();
    }
}