using System;

namespace Exa.Generics
{
    /// <summary>
    /// Base class for any exception that should be logged directly to the user
    /// </summary>
    public class UserException : Exception
    {
        public bool Fatal { get; private set; } = false;

        public UserException() 
            : base()
        { }

        public UserException(string message)
            : base(message)
        { }

        public UserException(string message, bool fatal)
            : base(message)
        {
            Fatal = fatal;
        }
    }
}