using System.Collections.Generic;

namespace Exa.IO
{
    public class DirectoryNode : IDirectoryNode
    {
        protected IDirectoryNode parent;
        private string name;

        public DirectoryNode(IDirectoryNode parent, string name) {
            this.parent = parent;
            this.name = name;
            IOUtils.EnsureCreated(GetPath());
        }

        public string GetPath() {
            return IOUtils.CombinePath(parent.GetPath(), name);
        }

        public IEnumerable<string> GetFiles(string pattern) {
            return System.IO.Directory.GetFiles(GetPath(), pattern);
        }

        public static implicit operator string(DirectoryNode node) {
            return node.GetPath();
        }
    }
}