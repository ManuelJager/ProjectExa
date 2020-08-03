using Exa.Debugging.Commands;
using UCommandConsole;
using UnityEngine;

namespace Exa.Debugging
{
    public class ExaConsole : MonoBehaviour
    {
        private Console console;

        private void Start()
        {
            console = Systems.MainUI.console;

            console.container.AddGetter(() => new ClsCommand());
            console.container.AddGetter(() => new ToggleDebugCommand());
            console.container.AddGetter(() => new SpawnCommand());
            console.container.AddGetter(() => new ToggleDiagnosticsCommand());
        }
    }
}