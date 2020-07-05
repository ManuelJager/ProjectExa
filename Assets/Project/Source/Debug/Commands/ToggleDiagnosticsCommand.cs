using Exa.Debugging.Commands.Parser;
using Exa.UI;

namespace Exa.Debugging.Commands
{
    public class ToggleDiagnosticsCommand : ParameterlessCommand
    {
        public override string Name => "td";
        public override string HelpText => "Toggles the diagnostics panel";

        public override void CommandAction()
        {
            var dp = DiagnosticsPanel.Instance.gameObject;
            dp.SetActive(!dp.activeSelf);
        }
    }
}