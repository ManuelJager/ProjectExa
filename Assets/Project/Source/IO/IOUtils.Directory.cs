using System.Collections.Generic;

namespace Exa.IO
{
    public static partial class IoUtils
    {
        static IoUtils()
        {
            globalDirectories = new Dictionary<string, ExaDirectory>
            {
                { "blueprints", new ExaDirectory(CombinePathWithDataPath("blueprints")) },
                { "settings", new ExaDirectory(CombinePathWithDataPath("settings")) },
                { "thumbnails", new ExaDirectory(CombinePathWithDataPath("thumbnails")) },
                { "defaultThumbnails", new ExaDirectory(CombinePathWithDataPath("defaultThumbnails")) }
            };
        }

        internal static Dictionary<string, ExaDirectory> globalDirectories;

        public static string GetPath(string name)
        {
            return globalDirectories[name].Value;
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