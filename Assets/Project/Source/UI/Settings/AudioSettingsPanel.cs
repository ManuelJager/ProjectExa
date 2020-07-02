using Exa.UI.Controls;

namespace Exa.UI.Settings
{
    public class AudioSettingsPanel : SettingsTab<ExaAudioSettings, AudioSettingsValues>
    {
        public ExaSlider masterVolumeSlider;
        public ExaSlider musicVolumeSlider;
        public ExaSlider effectsVolumeSlider;

        public override AudioSettingsValues GetSettingsValues()
        {
            return new AudioSettingsValues
            {
                masterVolume = masterVolumeSlider.Value,
                musicVolume = musicVolumeSlider.Value,
                effectsVolume = effectsVolumeSlider.Value
            };
        }

        public override void ReflectValues(AudioSettingsValues values)
        {
            masterVolumeSlider.Value = values.masterVolume;
            musicVolumeSlider.Value = values.musicVolume;
            effectsVolumeSlider.Value = values.effectsVolume;
        }
    }
}