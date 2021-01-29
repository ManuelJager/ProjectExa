using System.IO;
using System.Text;
using UnityEngine;

namespace Exa.IO
{
    public static class Tree
    {
        public static RootNode Root { get; }

        static Tree() {
#if UNITY_EDITOR
            var info = new DirectoryInfo(Application.persistentDataPath);
            var newName = $"{info.Name}Dev";
            // ReSharper disable once PossibleNullReferenceException
            Root = new RootNode(info.Parent.FullName, newName);
#else
            Root = new RootNode(Application.persistentDataPath);
#endif
        }

        public class RootNode : IDirectoryNode
        {
            private string rootFolder;

            public DirectoryNode Blueprints { get; }
            public DirectoryNode Settings { get; }
            public DirectoryNode Thumbnails { get; }
            public DirectoryNode DefaultThumbnails { get; }

            public RootNode(string rootFolder) {
                this.rootFolder = rootFolder;
                IOUtils.EnsureCreated(this.GetPath());

                Blueprints = new DirectoryNode(this, "blueprints");
                Settings = new DirectoryNode(this, "settings");
                Thumbnails = new DirectoryNode(this, "thumbnails");
                DefaultThumbnails = new DirectoryNode(this, "defaultThumbnails");
            }

            public RootNode(string rootFolder, string postFix)
                : this(IOUtils.CombinePath(rootFolder, postFix)) { }

            public StringBuilder GetPathBuilder() {
                return new StringBuilder(rootFolder);
            }
        }
    }
}