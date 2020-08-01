using Exa.Debugging.Commands;
using UCommandConsole;
using UnityEngine;

namespace Exa.Debugging
{
    public class ExaConsole : MonoBehaviour
    {
        [SerializeField] private Console console;

        private void Awake()
        {
            console.container.AddGetter(() => new ClsCommand());
            console.container.AddGetter(() => new ToggleDebugCommand());
            console.container.AddGetter(() => new SpawnCommand());
            console.container.AddGetter(() => new ToggleDiagnosticsCommand());
        }
    }
}