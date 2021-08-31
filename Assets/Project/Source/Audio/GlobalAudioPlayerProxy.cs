using UnityEngine;

namespace Exa.Audio {
    public class GlobalAudioPlayerProxy : MonoBehaviour {
        public void Play(Sound sound) {
            S.Audio.PlayGlobal(sound);
        }
    }
}