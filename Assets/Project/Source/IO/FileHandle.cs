using System;
using System.IO;

namespace Exa.IO
{
    /// <summary>
    /// Provides a reference to a file that may have its name updated
    /// </summary>
    public class FileHandle
    {
        private string currentPath;
        private Func<string, string> pathFactory;
        private Action<string> serializationFactory;
        private ISerializableItem item;

        public string TargetPath => pathFactory(item.ItemName);
        public bool PathIsDirty => currentPath != TargetPath;

        public FileHandle(ISerializableItem item, Func<string, string> pathFactory, Action<string> serializationFactory)
        {
            this.currentPath = pathFactory(item.ItemName);
            this.item = item;
            this.pathFactory = pathFactory;
            this.serializationFactory = serializationFactory;
        }

        public void Delete()
        {
            if (File.Exists(currentPath))
            {
                File.Delete(currentPath);
            }
        }

        public void UpdatePath()
        {
            if (PathIsDirty)
            {
                Delete();
            }
            currentPath = TargetPath;
            serializationFactory(currentPath);
        }
    }
}