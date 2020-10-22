using Exa.UI.Controls;
using System.Collections.Generic;

namespace Exa.UI.Settings
{
    public class AudioSettingsPanel : SettingsTab<ExaAudioSettings, AudioSettingsValues>
    {
        public SliderControl masterVolumeSlider;
        public SliderControl musicVolumeSlider;
        public SliderControl effectsVolumeSlider;

        public override AudioSettingsValues GetSettingsValues() =>
            new AudioSettingsValues
            {
                masterVolume = masterVolumeSlider.Value,
                musicVolume = musicVolumeSlider.Value,
                effectsVolume = effectsVolumeSlider.Value
            };

        public override void ReflectValues(AudioSettingsValues values)
        {
            masterVolumeSlider.SetValue(values.masterVolume, false);
            musicVolumeSlider.SetValue(values.musicVolume, false);
            effectsVolumeSlider.SetValue(values.effectsVolume, false);
        }
    }
}