using Exa.Data;
using UnityEngine;

namespace Exa.UI.Settings
{
    public class ExaAudioSettings : SaveableSettings<AudioSettingsValues>
    {
        public override AudioSettingsValues DefaultValues =>
            new AudioSettingsValues
            {
                masterVolume = 0.75f,
                musicVolume = 0.5f,
                effectsVolume = 0.5f
            };

        protected override string Key => "audioSettings";

        public override void Apply()
        {
            AudioListener.volume = Values.masterVolume;
            Systems.Audio.ST_AudioTrack.Volume = Values.musicVolume;
            Systems.Audio.UI_SFX_AudioTrack.Volume = Values.effectsVolume;
        }
    }
}