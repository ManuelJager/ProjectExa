using System.Collections.Generic;
using UnityEngine;

namespace Exa.Audio.Music
{
    [CreateAssetMenu(menuName = "Audio/TrackList")]
    public class TrackList : ScriptableObject
    {
        public List<Sound> tracks;
        public SoundHandle activeHandle;

        public Sound SelectSound()
        {
            var random = new System.Random();
            var index = random.Next(tracks.Count);
            return tracks[index];
        }
    }
}