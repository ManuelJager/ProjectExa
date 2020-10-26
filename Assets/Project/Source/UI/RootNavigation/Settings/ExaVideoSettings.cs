using Exa.Data;
#if !UNITY_EDITOR
using UnityEngine;
#endif

namespace Exa.UI.Settings
{
    public class ExaVideoSettings : SaveableSettings<VideoSettingsValues>
    {
        public override VideoSettingsValues DefaultValues =>
            new VideoSettingsValues {
                resolution = Resolutions.GetHighestSupportedResolution(),
                fullscreen = true
            };

        public Resolutions Resolutions { get; set; }

        protected override string Key => "videoSettings";

        public override void Apply() {
#if !UNITY_EDITOR
            Screen.SetResolution(
                width:                  Values.resolution.width,
                height:                 Values.resolution.height,
                fullscreen:             Values.fullscreen,
                preferredRefreshRate:   Values.resolution.refreshRate);
#endif
        }
    }
}