using Exa.Generics;
using System.Collections.Generic;
using System.IO;

namespace Exa.IO
{
    // TODO: optimize
    public class ExaDirectory
    {
        public string Value { get; private set; }

        internal ExaDirectory(string value)
        {
            EnsureCreated(value);
            this.Value = value;
        }

        internal ExaDirectory(string name, string value)
            : this(value)
        {
            IOUtils.GlobalDirectories[name] = this;
        }

        public static ExaDirectory Create(string value)
        {
            return new ExaDirectory(value);
        }

        public static ExaDirectory CreateAndRegister(string name, string value)
        {
            return new ExaDirectory(name, value);
        }

        private static void EnsureCreated(string directoryString)
        {
            if (!Directory.Exists(directoryString))
            {
                Directory.CreateDirectory(directoryString);
            }
        }
    }
}