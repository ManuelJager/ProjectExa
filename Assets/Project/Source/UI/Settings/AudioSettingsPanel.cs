using Exa.UI.Controls;
using System.Collections.Generic;

namespace Exa.UI.Settings
{
    public class AudioSettingsPanel : SettingsTab<ExaAudioSettings, AudioSettingsValues>
    {
        public SliderControl masterVolumeSlider;
        public SliderControl musicVolumeSlider;
        public SliderControl effectsVolumeSlider;

        private InputControl[] controls;

        private void Awake()
        {
            controls = new InputControl[]
            {
                masterVolumeSlider,
                musicVolumeSlider,
                effectsVolumeSlider
            };
        }

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

        protected override IEnumerable<InputControl> GetControls()
        {
            return controls;
        }
    }
}