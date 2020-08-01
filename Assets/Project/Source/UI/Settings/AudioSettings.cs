using Exa.Data;
using UnityEngine;

namespace Exa.UI.Settings
{
    public class AudioSettingsValues
    {
        public float masterVolume;
        public float musicVolume;
        public float effectsVolume;
    }

    public class ExaAudioSettings : SaveableSettings<AudioSettingsValues>
    {
        public override AudioSettingsValues DefaultValues => new AudioSettingsValues
        {
            masterVolume = 0.75f,
            musicVolume = 0.5f,
            effectsVolume = 0.5f
        };

        protected override string Key => "audioSettings";

        public override void Apply()
        {
            AudioListener.volume = Values.masterVolume;
            MainManager.Instance.audioManager.ST_AudioTrack.Volume = Values.musicVolume;
            MainManager.Instance.audioManager.UI_SFX_AudioTrack.Volume = Values.effectsVolume;
        }
    }
}