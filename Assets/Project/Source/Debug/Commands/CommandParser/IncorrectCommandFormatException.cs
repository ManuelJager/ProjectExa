using System;

namespace Exa.Debug.Commands.Parser
{
    public sealed class IncorrectCommandFormatException : Exception
    {
        public IncorrectCommandFormatException(string message)
            : base(message)
        {
        }
    }
}