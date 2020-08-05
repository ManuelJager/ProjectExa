using System.Collections.Generic;

namespace Exa.IO
{
    public static partial class IOUtils
    {
        static IOUtils()
        {
            GlobalDirectories = new Dictionary<string, ExaDirectory>
            {
                { "blueprints", new ExaDirectory(CombinePathWithDataPath("blueprints")) },
                { "settings", new ExaDirectory(CombinePathWithDataPath("settings")) },
                { "thumbnails", new ExaDirectory(CombinePathWithDataPath("thumbnails")) },
            };
        }

        internal static Dictionary<string, ExaDirectory> GlobalDirectories;

        public static string GetPath(string name)
        {
            return GlobalDirectories[name].Value;
        }

        public static string CombineWithDirectory(string name, params string[] paths)
        {
            var pathList = new List<string>();
            pathList.Add(GetPath(name));
            pathList.AddRange(paths);

            return CombinePath(pathList.ToArray());
        }
    }
}