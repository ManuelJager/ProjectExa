using System;
using UnityEngine;

namespace Exa.UI.Settings
{
    public class VideoSettingsValues : IEquatable<VideoSettingsValues>
    {
        public Resolution resolution;
        public bool fullscreen;

        public bool Equals(VideoSettingsValues other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return resolution.Equals(other.resolution) && fullscreen == other.fullscreen;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((VideoSettingsValues) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (resolution.GetHashCode() * 397) ^ fullscreen.GetHashCode();
            }
        }
    }
}