using System;
using System.Text;

namespace Exa.Utils
{
    public static class StringExtensions
    {
        public static char GetRandomChar()
        {
            var chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";
            var r = new Random();
            var i = r.Next(chars.Length);
            return chars[i];
        }

        public static void AppendLineIndented(this StringBuilder sb, string line, int tabs = 0)
        {
            var tabbedString = new string('\t', tabs);
            var newString = line.Replace("\n", $"\n{tabbedString}").TrimEnd();
            sb.AppendLine($"{tabbedString}{newString}");
        }
    }
}