using System;

namespace Exa.UI.Settings
{
    public class AudioSettingsValues : IEquatable<AudioSettingsValues>
    {
        public float masterVolume;
        public float musicVolume;
        public float effectsVolume;

        public bool Equals(AudioSettingsValues other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return masterVolume.Equals(other.masterVolume) && musicVolume.Equals(other.musicVolume) && effectsVolume.Equals(other.effectsVolume);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AudioSettingsValues) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = masterVolume.GetHashCode();
                hashCode = (hashCode * 397) ^ musicVolume.GetHashCode();
                hashCode = (hashCode * 397) ^ effectsVolume.GetHashCode();
                return hashCode;
            }
        }
    }
}