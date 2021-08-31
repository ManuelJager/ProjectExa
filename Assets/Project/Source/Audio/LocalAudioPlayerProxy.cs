using System.Collections.Generic;
using UnityEngine;

namespace Exa.Audio {
    public class LocalAudioPlayerProxy : MonoBehaviour, ITrackContext {
        private Dictionary<string, AudioSource> players;
        private SoundHandleGroupDictionary handles;

        public SoundHandleGroupDictionary Handles {
            get => handles;
        }
        
        private void Awake() {
            players = new Dictionary<string, AudioSource>();
            handles = new SoundHandleGroupDictionary();
        }
        
        public SoundHandle Play(Sound sound) {
            if (!players.ContainsKey(sound.Id)) {
                players[sound.Id] = gameObject.AddComponent<AudioSource>();
            }

            var handler = new SoundHandle {
                sound = sound,
                audioSource = players[sound.Id]
            };
            
            handler.Play(this);

            return handler;
        }

        public void RegisterHandle(SoundHandle soundHandle) {
            soundHandle.RegisterHandle(handles);
        }

        public void StopAllSounds() {
            throw new System.NotImplementedException();
        }
    }
}