using Exa.UI.Components;
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

        private InputControl[] _controls;

        private void Awake()
        {
            _controls = new InputControl[]
            {
                resolutionDropdown,
                refreshRatesDropdown,
                fullscreenRadio
            };

            refreshRatesDropdown.OnValueChange.AddListener((obj) =>
            {
                resolutionDropdown.FilterByRefreshRate((int)obj);
                resolutionDropdown.SelectFirst();
            });
        }

        public override VideoSettingsValues GetSettingsValues()
        {
            return new VideoSettingsValues
            {
                resolution = (Resolution)resolutionDropdown.Value,
                fullscreen = fullscreenRadio.Value
            };
        }

        public override void ReflectValues(VideoSettingsValues values)
        {
            resolutionDropdown.SetSelected(values.resolution);
            refreshRatesDropdown.SetSelected(values.resolution.refreshRate);
            fullscreenRadio.Value = values.fullscreen;
        }

        protected override IEnumerable<InputControl> GetControls()
        {
            return _controls;
        }
    }
}