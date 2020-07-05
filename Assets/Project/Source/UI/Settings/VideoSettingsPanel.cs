using Exa.UI.Controls;
using UnityEngine;

namespace Exa.UI.Settings
{
    public class VideoSettingsPanel : SettingsTab<ExaVideoSettings, VideoSettingsValues>
    {
        [SerializeField] public ResolutionDropdown resolutionDropdown;
        [SerializeField] public Dropdown refreshRatesDropdown;
        [SerializeField] public Radio fullscreenRadio;

        private void Awake()
        {
            refreshRatesDropdown.onDropdownTabValueSelected.AddListener((obj) =>
            {
                resolutionDropdown.FilterByRefreshRate((int)obj);
                resolutionDropdown.SelectFirstActive();
            });
        }

        public override VideoSettingsValues GetSettingsValues()
        {
            return new VideoSettingsValues
            {
                resolution = (Resolution)resolutionDropdown.Value.Value,
                fullscreen = fullscreenRadio.Value
            };
        }

        public override void ReflectValues(VideoSettingsValues values)
        {
            resolutionDropdown.SetSelected(values.resolution);
            refreshRatesDropdown.SetSelected(values.resolution.refreshRate);
            fullscreenRadio.Value = fullscreenRadio.Value;
        }
    }
}