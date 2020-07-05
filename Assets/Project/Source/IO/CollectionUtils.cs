using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Exa.IO
{
    public static class CollectionUtils
    {
        /// <summary>
        /// Serialize to json and write a collection to a directory
        /// <para>
        /// Optionally deletes files not present in collection
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">Collection to be saved</param>
        /// <param name="directory">Directory path</param>
        /// <param name="deleteOldFiles">Wether items that are present in the directory but aren't in the collection should be deleted</param>
        public static void SaveCollectionToDirectory<T>(ICollection<T> collection, string directory, bool deleteOldFiles = false)
            where T : ISerializationCollectionItem
        {
            if (Directory.Exists(directory) && deleteOldFiles)
            {
                // Get names of the items that need to be saved
                var itemNames = collection.Select(item => Path.Combine(directory, $"{item.itemName}.json"));
                // Get names of the files that are already saved
                var fileNames = Directory.GetFiles(directory);
                // Get file names that were in the collection, but not anymore
                var oldFileNames = fileNames.Except(itemNames);
                foreach (var oldFileName in oldFileNames)
                {
                    File.Delete(oldFileName);
                }
            }

            foreach (var item in collection)
            {
                var itemPath = IOUtils.CombinePathWithDataPath(directory, $"{item.itemName}.json");

                IOUtils.JsonSerializeToPath(item, itemPath);
            }
        }

        /// <summary>
        /// Deserialize items from directory and add to collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="directory"></param>
        public static void LoadToCollectionFromDirectory<T>(ICollection<T> collection, string directory)
            where T : class, ISerializationCollectionItem
        {
            if (!Directory.Exists(directory)) return;

            foreach (var filePath in Directory.GetFiles(directory))
            {
                T item;

                IOUtils.TryJsonDeserializeFromPath(filePath, out item);

                if (item != null)
                {
                    collection.Add(item);
                }
            }
        }
    }
}