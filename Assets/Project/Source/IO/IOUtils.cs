using System.IO;
using System.Linq;
using UnityEngine;

namespace Exa.IO
{
    public static partial class IOUtils
    {
        public static string CombinePathWithDataPath(params string[] paths)
        {
            paths = paths
                .ToList()
                .Prepend(Application.persistentDataPath)
                .ToArray();

            return CombinePath(paths);
        }

        public static string CombinePath(params string[] paths)
        {
            return Path.Combine(paths).Replace("/", "\\");
        }
    }
}