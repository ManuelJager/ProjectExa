using CommandEngine.Models;
using Exa.Debug.DebugConsole;

namespace Exa.Debug.Commands
{
    public class ClsCommand : ParameterfulCommand
    {
        public override string Name => "cls";
        public override string HelpText => "Clears the console";

        public override void CommandAction()
        {
            DebugConsoleController.Instance.ConsoleOutputText = "";
        }
    }
}