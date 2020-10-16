using System.IO;
using System.Linq;
using UnityEngine;

namespace Exa.IO
{
    public static partial class IOUtils
    {
        public static string CombinePathWithDataPath(params string[] paths)
        {
            paths = paths.Prepend(Application.persistentDataPath)
                .ToArray();

            return CombinePath(paths);
        }

        public static string CombinePath(params string[] paths)
        {
            return Path.Combine(paths).Replace("/", "\\");
        }

        public static void SaveTexture2D(Texture2D tex, string filePath)
        {
            var bytes = tex.EncodeToPNG();
            File.WriteAllBytes(filePath, bytes);
        }

        public static Texture2D LoadTexture2D(string filePath, int width, int height)
        {
            var bytes = File.ReadAllBytes(filePath);
            var tex = new Texture2D(width, height);
            tex.LoadImage(bytes);

            return tex;
        }
    }
}