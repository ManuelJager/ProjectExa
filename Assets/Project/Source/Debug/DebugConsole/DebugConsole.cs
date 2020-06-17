using CommandEngine;
using Exa.Debug.Commands;

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