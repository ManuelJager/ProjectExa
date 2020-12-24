using System;
using System.IO;

namespace Exa.IO
{
    public class ExaDirectory
    {
        public string Value { get; }

        internal ExaDirectory(string value, bool combineWithDataPath = true)
        {
            var path = combineWithDataPath 
                ? IOUtils.CombinePathWithDataPath(value) 
                : value;

            EnsureCreated(path);
            this.Value = path;
        }

        internal ExaDirectory(ExaDirectory parent, string value)
        {
            var path = IOUtils.CombinePath(parent, value);
            EnsureCreated(path);
            this.Value = path;
        }

        public string CombineWith(string fileName) {
            return IOUtils.CombinePath(this, fileName);
        }

        private static void EnsureCreated(string directoryString) {
            if (!Directory.Exists(directoryString)) {
                Directory.CreateDirectory(directoryString);
            }
        }

        public static implicit operator string(ExaDirectory directory) {
            if (directory == null)
                throw new ArgumentNullException(nameof(directory));

            return directory.Value;
        }
    }
}