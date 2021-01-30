using System;
using UnityEngine;

namespace Exa.UI.Settings
{
    public class VideoSettingsValues : IEquatable<VideoSettingsValues>
    {
        public Resolution resolution;
        public bool fullscreen;

        public bool Equals(VideoSettingsValues other) {
            return resolution.Equals(other?.resolution) && fullscreen.Equals(other?.fullscreen);
        }
    }
}