using System.Linq;
using Exa.Audio.Music;
using Exa.Types.Generics;
using Exa.UI.Controls;

namespace Exa.UI.Settings
{
    public class AudioSettingsPanel : SettingsTab<ExaAudioSettings, AudioSettingsValues>
    {
        public SliderControl masterVolumeSlider;
        public SliderControl musicVolumeSlider;
        public SliderControl effectsVolumeSlider;
        public DropdownControl soundTrackNameDropdown;

        public override void Init() {
            CreateSoundTrackDropdownTabs();
            base.Init();
        }

        public override AudioSettingsValues GetSettingsValues() =>
            new AudioSettingsValues {
                masterVolume = masterVolumeSlider.Value / 100f,
                musicVolume = musicVolumeSlider.Value / 100f,
                effectsVolume = effectsVolumeSlider.Value / 100f,
                soundTrackName = soundTrackNameDropdown.Value as string
            };

        public override void ReflectValues(AudioSettingsValues values) {
            masterVolumeSlider.SetValue(values.masterVolume * 100f, false);
            musicVolumeSlider.SetValue(values.musicVolume * 100f, false);
            effectsVolumeSlider.SetValue(values.effectsVolume * 100f, false);
            soundTrackNameDropdown.SetValue(values.soundTrackName, false);
        }

        private void CreateSoundTrackDropdownTabs() {
            var items = Systems.Audio.Music.Provider.GetSoundTrackDescriptions()
                .Select(description => description.GetLabeledValue());
            soundTrackNameDropdown.CreateTabs(items);
        }
    }
}