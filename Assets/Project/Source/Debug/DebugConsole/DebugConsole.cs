using CommandEngine;
using Exa.Debug.Commands;

namespace Exa.Debug.DebugConsole
{
    public class DebugConsole : Console
    {
        public DebugConsole()
        {
            CommandFactory.AddToConsole(this);
        }
    }
}