using System;
using System.IO;

namespace Exa.IO
{
    public static class CollectionUtils
    {
        /// <summary>
        /// Deserialize items from directory and add to collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="directory"></param>
        public static void LoadJsonCollectionFromDirectory<T>(string directory, Action<T> callback)
            where T : class
        {
            if (!Directory.Exists(directory)) return;

            foreach (var filePath in Directory.GetFiles(directory, "*.json"))
            {
                IOUtils.TryJsonDeserializeFromPath(filePath, out T item);

                callback(item);
            }
        }

        public static void GetJsonFilesFromDirectory<T>(string directory, Action<string> callback)
            where T : class
        {
            if (!Directory.Exists(directory)) return;

            foreach (var filePath in Directory.GetFiles(directory, "*.json"))
            {
                callback(filePath);
            }
        }
    }
}