using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Audio.Music
{
    public enum Atmosphere
    {
        MainMenu,
        None
    }

    public class MusicTrack : AudioTrack
    {
        [SerializeField] private TrackList MainMenuTrackList;
        private Atmosphere activeAtmosphere = Atmosphere.None;

        public void SetAtmosphere(Atmosphere atmosphere) {
            if (activeAtmosphere == atmosphere) return;

            // Fade out the current atmosphere
            ProcessOldAtmosphere(activeAtmosphere);

            // Fade in the new atmosphere
            ProcessNewAtmosphere(atmosphere);

            activeAtmosphere = atmosphere;
        }

        private void ProcessNewAtmosphere(Atmosphere newAtmosphere) {
            // since no sounds are related to this atmosphere, Atmosphere.None is just a no-op
            if (newAtmosphere == Atmosphere.None) return;

            // Get the tracklist, select a track and play it
            var trackList = GetTrackList(newAtmosphere);
            var track = trackList.SelectSound();
            var handle = PlayGlobal(track);
            trackList.activeHandle = handle;
        }

        private void ProcessOldAtmosphere(Atmosphere oldAtmosphere) {
            // since no sounds are related to this atmosphere, Atmosphere.None is just a no-op
            if (oldAtmosphere == Atmosphere.None) return;

            // Get the tracklist and stop the current playing track
            var trackList = GetTrackList(oldAtmosphere);
            var handle = trackList.activeHandle;
            handle?.Stop();
        }

        private TrackList GetTrackList(Atmosphere atmosphere) {
            switch (atmosphere) {
                case Atmosphere.MainMenu:
                    return MainMenuTrackList;

                default:
                    return null;
            }
        }
    }
}