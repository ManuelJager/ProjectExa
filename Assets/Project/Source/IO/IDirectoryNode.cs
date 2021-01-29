using System.Text;

namespace Exa.IO
{
    public interface IDirectoryNode
    {
        public StringBuilder GetPathBuilder();
    }

    public static class IDirectoryNodeExtensions
    {
        public static string GetPath(this IDirectoryNode node) {
            return node.GetPathBuilder().ToString();
        }

        public static string CombineWith(this IDirectoryNode node, string fileName) {
            return IOUtils.CombinePath(node.GetPath(), fileName);
        }
    }
}