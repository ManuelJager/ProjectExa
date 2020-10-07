using Exa.Debugging.Commands;
using UCommandConsole;
using UnityEngine;

namespace Exa.Debugging
{
    public class ExaConsole : MonoBehaviour
    {
        private Console _console;

        private void Start()
        {
            _console = Systems.Ui.console;

            _console.Container.AddGetter(() => new ClsCommand());
            _console.Container.AddGetter(() => new SpawnCommand());
            _console.Container.AddGetter(() => new ToggleDiagnosticsCommand());
        }
    }
}