using System.IO;

namespace Exa.IO
{
    // TODO: optimize
    public class RelativeDir
    {
        public string value;

        static RelativeDir()
        {
            Create(USER_BLUEPRINTS);
            Create(THUMBNAILS);
        }

        public static void Create(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public static string USER_BLUEPRINTS => IOUtils.CombinePathWithDataPath("userBlueprints");

        public static string THUMBNAILS => IOUtils.CombinePathWithDataPath("thumbnails");
    }
}