namespace Exa.UI.Settings
{
    public class VideoSettingsPanel : SettingsTab<ExaVideoSettings, VideoSettingsValues>
    {
        public override VideoSettingsValues GetSettingsValues()
        {
            return new VideoSettingsValues
            {
            };
        }

        public override void ReflectValues(VideoSettingsValues values)
        {
        }
    }
}