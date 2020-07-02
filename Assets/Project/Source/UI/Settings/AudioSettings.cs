namespace Exa.UI.Settings
{
    public struct AudioSettingsValues
    {
        public float masterVolume;
        public float musicVolume;
        public float effectsVolume;
    }

    public class ExaAudioSettings : SaveableSettings<AudioSettingsValues>
    {
        public override AudioSettingsValues DefaultValues => new AudioSettingsValues
        {
            masterVolume = 0.5f,
            musicVolume = 0.5f,
            effectsVolume = 0.5f
        };

        public override void Apply()
        {
        }
    }
}