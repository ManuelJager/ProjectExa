using System;

namespace Exa.Debug.Commands.Parser
{
    public sealed class IncorrectModelFormatException : Exception
    {
        public IncorrectModelFormatException(string message)
            : base(message)
        {
        }
    }
}