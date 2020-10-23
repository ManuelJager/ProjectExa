using Exa.UI.Controls;
using UnityEngine;

namespace Exa.UI.Settings
{
    public class VideoSettingsPanel : SettingsTab<ExaVideoSettings, VideoSettingsValues>
    {
        public ResolutionDropdown resolutionDropdown;
        public DropdownControl refreshRatesDropdown;
        public RadioControl fullscreenRadio;

        private void Awake() {
            refreshRatesDropdown.OnValueChange.AddListener(obj => {
                resolutionDropdown.FilterByRefreshRate((int) obj);
                resolutionDropdown.SelectFirst();
            });
        }

        public override VideoSettingsValues GetSettingsValues() =>
            new VideoSettingsValues {
                resolution = (Resolution) resolutionDropdown.Value,
                fullscreen = fullscreenRadio.Value
            };

        public override void ReflectValues(VideoSettingsValues values) {
            // TODO: Notify user of invalid configuration
            if (!resolutionDropdown.ContainsItem(values.resolution))
                values.resolution = settings.DefaultValues.resolution;

            resolutionDropdown.SetValue(values.resolution, false);
            refreshRatesDropdown.SetValue(values.resolution.refreshRate, false);
            fullscreenRadio.SetValue(values.fullscreen, false);
        }

        public override void Init() {
            settings.Resolutions = new Resolutions();
            refreshRatesDropdown.CreateTabs(settings.Resolutions.GetRefreshRateLabels());
            // Get first refresh rate
            var firstRefreshRate = (int) refreshRatesDropdown.Value;

            resolutionDropdown.CreateTabs(settings.Resolutions.GetResolutionLabels());
            resolutionDropdown.FilterByRefreshRate(firstRefreshRate);

            base.Init();
        }
    }
}