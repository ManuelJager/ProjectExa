using System;
using System.IO;

namespace Exa.IO
{
    /// <summary>
    /// Provides a reference to a file that may have its name updated
    /// </summary>
    public class FileHandle
    {
        private readonly Func<string, string> _pathFactory;
        private readonly Action<string> _serializationFactory;
        private readonly ISerializableItem _item;

        public string CurrentPath { get; set; }
        public string TargetPath => _pathFactory(_item.ItemName);
        public bool PathIsDirty => CurrentPath != TargetPath;

        public FileHandle(ISerializableItem item, Func<string, string> pathFactory, Action<string> serializationFactory, bool generatePath = true)
        {
            this._item = item;
            this._pathFactory = pathFactory;
            this._serializationFactory = serializationFactory;

            if (generatePath)
            {
                this.CurrentPath = pathFactory(item.ItemName);
            }
        }

        public void Delete()
        {
            if (CurrentPath != "" && File.Exists(CurrentPath))
            {
                File.Delete(CurrentPath);
            }
        }

        public void Refresh()
        {
            if (PathIsDirty)
            {
                Delete();
            }
            CurrentPath = TargetPath;
            _serializationFactory(CurrentPath);
        }
    }
}