using System;

namespace Exa.Debugging.Commands.Parser
{
    public sealed class IncorrectModelFormatException : Exception
    {
        public IncorrectModelFormatException(string message)
            : base(message)
        {
        }
    }
}