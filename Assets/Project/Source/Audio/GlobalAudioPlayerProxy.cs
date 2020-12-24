using UnityEngine;

namespace Exa.Audio
{
    public class GlobalAudioPlayerProxy : MonoBehaviour
    {
        public void Play(string soundId) {
            Systems.Audio.PlayGlobal(soundId);
        }
    }
}