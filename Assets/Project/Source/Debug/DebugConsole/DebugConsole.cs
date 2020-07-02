using Exa.Debug.Commands.Parser;

namespace Exa.Debug
{
    public class DebugConsole : Console
    {
        public DebugConsole()
        {
            CommandFactory.AddToConsole(this);
        }
    }
}