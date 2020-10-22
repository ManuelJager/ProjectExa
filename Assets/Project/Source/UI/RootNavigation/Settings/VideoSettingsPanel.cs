using Exa.UI.Controls;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.UI.Settings
{
    public class VideoSettingsPanel : SettingsTab<ExaVideoSettings, VideoSettingsValues>
    {
        public ResolutionDropdown resolutionDropdown;
        public DropdownControl refreshRatesDropdown;
        public RadioControl fullscreenRadio;

        private void Awake()
        {
            refreshRatesDropdown.OnValueChange.AddListener(obj =>
            {
                resolutionDropdown.FilterByRefreshRate((int)obj);
                resolutionDropdown.SelectFirst();
            });
        }

        public override VideoSettingsValues GetSettingsValues() =>
            new VideoSettingsValues
            {
                resolution = (Resolution)resolutionDropdown.Value,
                fullscreen = fullscreenRadio.Value
            };

        public override void ReflectValues(VideoSettingsValues values)
        {
            // TODO: Notify user of invalid configuration
            if (!resolutionDropdown.ContainsItem(values.resolution))
                values.resolution = settings.DefaultValues.resolution;

            resolutionDropdown.SetValue(values.resolution, false);
            refreshRatesDropdown.SetValue(values.resolution.refreshRate, false);
            fullscreenRadio.SetValue(values.fullscreen, false);
        }
    }
}