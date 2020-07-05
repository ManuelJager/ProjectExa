using Exa.Debugging.Commands.Parser;

namespace Exa.Debugging
{
    public class DebugConsole : Console
    {
        public DebugConsole()
        {
            CommandFactory.AddToConsole(this);
        }
    }
}