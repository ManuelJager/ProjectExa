using System.Collections;
using System.Text;

namespace Exa.Utils
{
    public static class StringBuilderExtensions
    {
        public static void AppendLineIndented(this StringBuilder sb, string line, int tabs = 0)
        {
            var tabbedString = new string('\t', tabs);
            var newString = line.Replace("\n", $"\n{tabbedString}").TrimEnd();
            sb.AppendLine($"{tabbedString}{newString}");
        }
    }
}