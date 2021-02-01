using System;
using System.Collections;
using Exa.Audio.Music;
using Exa.Data;
using UnityEngine;

namespace Exa.UI.Settings
{
    public class ExaAudioSettings : SaveableSettings<AudioSettingsValues>
    {
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
            Systems.Audio.Music.CurrentSoundtrack = GetSoundTrack();
        }

        private ISoundTrack GetSoundTrack() {
            var provider = Systems.Audio.Music.Provider;
            var soundTrack = provider.Find(Values.soundTrackName);
            if (soundTrack == null) {
                Systems.UI.logger.Log($"Could not find soundtrack by name \"{Values.soundTrackName}\"");
                return provider.DefaultSoundTrack;
            }

            var customSoundTrack = soundTrack.GetSoundTrack(GetProgressReporter(), out var enumerator);
            Systems.Instance.StartCoroutine(enumerator);
            return customSoundTrack;
        }

        private string GetDefaultSoundTrackName() {
            return Systems.Audio.Music.Provider.DefaultSoundTrack.Description.Name;
        }

        private IProgress<float> GetProgressReporter() {
            return new Progress<float>((progress) => Debug.Log($"Progress: {progress}"));
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