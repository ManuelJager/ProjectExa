using Exa.Debugging.Commands;
using UCommandConsole;
using UnityEngine;

namespace Exa.Debugging
{
    public class ExaConsole : MonoBehaviour
    {
        private Console console;

        private void Start() {
            console = Systems.UI.console;

            console.Container.AddGetter(() => new ClsCommand());
            console.Container.AddGetter(() => new SpawnCommand());
            console.Container.AddGetter(() => new ToggleDiagnosticsCommand());
        }
    }
}