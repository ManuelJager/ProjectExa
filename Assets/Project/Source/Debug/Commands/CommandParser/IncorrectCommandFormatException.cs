using System;

namespace Exa.Debugging.Commands.Parser
{
    public sealed class IncorrectCommandFormatException : Exception
    {
        public IncorrectCommandFormatException(string message)
            : base(message)
        {
        }
    }
}