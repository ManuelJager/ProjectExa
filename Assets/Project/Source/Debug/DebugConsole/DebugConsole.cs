using CommandEngine;

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