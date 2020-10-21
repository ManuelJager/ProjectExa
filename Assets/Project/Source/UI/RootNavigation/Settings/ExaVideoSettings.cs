using Exa.Data;
using System.Linq;
using UnityEngine;

namespace Exa.UI.Settings
{
    public class VideoSettingsValues
    {
        public Resolution resolution;
        public bool fullscreen;
    }

    public class ExaVideoSettings : SaveableSettings<VideoSettingsValues>
    {
        public override VideoSettingsValues DefaultValues => new VideoSettingsValues
        {
            resolution = Screen.resolutions.First(),
            fullscreen = false
        };

        public override void Load()
        {
            base.Load();
            if (!Screen.resolutions.Contains(Values.resolution))
            {
                Values.resolution = DefaultValues.resolution;
            }
        }

        protected override string Key => "videoSettings";

        public override void Apply()
        {
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