using System;
using System.Text;

namespace Exa.Utils
{
    public static class StringExtensions
    {
        public static char GetRandomChar() {
            var chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";
            var r = new Random();
            var i = r.Next(chars.Length);
            return chars[i];
        }

        public static void AppendLineIndented(this StringBuilder sb, string line, int tabs = 0) {
            var tabbedString = new string('\t', tabs);
            var newString = line.Replace("\n", $"\n{tabbedString}").TrimEnd();
            sb.AppendLine($"{tabbedString}{newString}");
        }

        public static string Format(this string format, params object[] args) {
            return string.Format(format, args);
        }

        public static string ToGenericString(this Type type) {
            if (type.IsGenericType) {
                var retType = new StringBuilder();
                var parentType = type.FullName.Split('`');
                // We will build the type here.
                var arguments = type.GetGenericArguments();
                var argList = new StringBuilder();
                foreach (var t in arguments) {
                    // Let's make sure we get the argument list.
                    var arg = ToGenericString(t);
                    if (argList.Length > 0) {
                        argList.AppendFormat(", {0}", arg);
                    }
                    else {
                        argList.Append(arg);
                    }
                }

                if (argList.Length > 0) {
                    retType.AppendFormat("{0}<{1}>", parentType[0], argList);
                }

                return retType.ToString();
            }
            else {
                return type.ToString();
            }
        }
    }
}