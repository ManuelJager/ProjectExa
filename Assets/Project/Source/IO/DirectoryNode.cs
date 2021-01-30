using System.Text;

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

        public static implicit operator string(DirectoryNode node) {
            return node.GetPath();
        }
    }
}