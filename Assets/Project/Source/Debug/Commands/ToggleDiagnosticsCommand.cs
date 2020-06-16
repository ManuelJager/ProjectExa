using CommandEngine.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Debug.Commands
{
    public class ToggleDiagnosticsCommand : ParameterlessCommand
    {
        public override string Name => "td";
        public override string HelpText => "Toggles the diagnostics panel";

        public override void CommandAction()
        {
            var dp = GameManager.Instance.diagnosticsPanel.gameObject;
            dp.SetActive(!dp.activeSelf);
        }
    }
}

