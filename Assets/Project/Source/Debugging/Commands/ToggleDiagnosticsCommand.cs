using Exa.UI;
using UCommandConsole;

namespace Exa.Debugging.Commands
{
    public class ToggleDiagnosticsCommand : Command
    {
        public override string GetName() => "tgl-diag";

        public override void Execute(Console host)
        {
            var dp = Systems.Ui.diagnostics.gameObject;
            dp.SetActive(!dp.activeSelf);
        }
    }
}