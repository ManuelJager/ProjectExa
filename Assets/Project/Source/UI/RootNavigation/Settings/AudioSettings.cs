using System;
using System.Collections;
using Exa.Audio;
using Exa.Audio.Music;
using Exa.Data;
using Exa.Utils;
using UnityEngine;

namespace Exa.UI.Settings
{
    public class ExaAudioSettings : SaveableSettings<AudioSettingsValues>
    {
        public SoundTrackLoadHandler LoadHandler { get; set; }

        public override AudioSettingsValues DefaultValues => new AudioSettingsValues {
            masterVolume = 0.75f,
            musicVolume = 0.5f,
            effectsVolume = 0.5f,
            soundTrackName = GetDefaultSoundTrackName()
        };

        protected override string Key => "audioSettings";

        public override void Apply() {
            AudioListener.volume = Values.masterVolume;
            Systems.Audio.Music.Volume = Values.musicVolume;
            Systems.Audio.Effects.Volume = Values.effectsVolume;
            LoadSoundTrack();
        }

        private void LoadSoundTrack() {
            if (LoadHandler == null) {
                throw new InvalidOperationException("Load handler not set when loading soundtrack");
            }

            var music = Systems.Audio.Music;

            if (music.CurrentSoundtrack != null && 
                music.CurrentSoundtrack.Description.Name == Values.soundTrackName) {
                return;
            }

            var provider = music.Provider;

            // Make sure we can atleast load the default soundtrack
            var description = provider.Find(Values.soundTrackName);
            if (description == null) {
                Systems.UI.logger.LogException($"Could not find soundtrack by name \"{Values.soundTrackName}\"");
                provider.DefaultSoundTrack.LoadSoundTrack(LoadHandler);
            }
            else {
                description.LoadSoundTrack(LoadHandler);
            }

            LoadHandler.LoadEnumerator = LoadHandler.LoadEnumerator.Then(() => {
                Systems.Audio.Music.CurrentSoundtrack = LoadHandler.OutputSoundtrack;
                LoadHandler.LoadEnumerator = null;
            });
        }

        private string GetDefaultSoundTrackName() {
            return Systems.Audio.Music.Provider.DefaultSoundTrack.Name;
        }

        public override AudioSettingsValues Clone() => new AudioSettingsValues {
            masterVolume = Values.masterVolume,
            effectsVolume = Values.effectsVolume,
            musicVolume = Values.musicVolume,
            soundTrackName = Values.soundTrackName
        };

        protected override AudioSettingsValues DeserializeValues(string path) {
            var values = base.DeserializeValues(path);

            if (Systems.Audio.Music.Provider.Find(values.soundTrackName) == null) {
                values.soundTrackName = GetDefaultSoundTrackName();
            }

            return values;
        }
    }
}