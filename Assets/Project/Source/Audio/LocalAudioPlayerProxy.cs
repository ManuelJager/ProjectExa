using Exa.Types.Generics;
using UnityEditor;
using UnityEngine;

namespace Exa.Audio {
    public class LocalAudioPlayerProxy : MonoBehaviour, ITrackContext {
        [SerializeField] private SerializableDictionary<Sound, AudioSource> players;
        private SoundHandleGroupDictionary handles;

        public SoundHandleGroupDictionary Handles {
            get => handles;
        }
        
        private void Awake() {
            handles = new SoundHandleGroupDictionary();

            foreach (var sound in players.Keys) {
                handles.RegisterGroup(sound.Id);
            }
        }
        
        public SoundHandle Play(Sound sound, float progress = 0f) {
            var handler = new SoundHandle {
                sound = sound,
                audioSource = players[sound]
            };
            
            handler.Play(this, progress);

            return handler;
        }

        public void RegisterHandle(SoundHandle soundHandle) {
            soundHandle.RegisterHandle(handles);
        }

        public void StopAllSounds() {
            handles.Stop();
        }
        
    #if UNITY_EDITOR
        [ContextMenu(nameof(GenerateAudioSources))]
        public void GenerateAudioSources() {
            foreach (var sound in players.Keys) {
                var player = players[sound];
                
                if (player == null) {
                    player = gameObject.AddComponent<AudioSource>();
                    players[sound] = player;
                }

                player.clip = sound.AudioClip;
            }
            
            EditorUtility.SetDirty(gameObject);
        }    
    #endif
    }
}