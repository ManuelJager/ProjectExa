using Exa.UI;
using UCommandConsole;

namespace Exa.Debugging.Commands
{
    public class ToggleDiagnosticsCommand : Command
    {
        public override string GetName() => "td";

        public override void Execute(Console host)
        {
            var dp = Systems.MainUI.diagnostics.gameObject;
            dp.SetActive(!dp.activeSelf);
        }
    }
}