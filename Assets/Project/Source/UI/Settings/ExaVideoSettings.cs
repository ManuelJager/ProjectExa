namespace Exa.UI.Settings
{
    public struct VideoSettingsValues
    {
    }

    public class ExaVideoSettings : SaveableSettings<VideoSettingsValues>
    {
        public override VideoSettingsValues DefaultValues => new VideoSettingsValues
        {
        };

        public override void Apply()
        {
        }
    }
}