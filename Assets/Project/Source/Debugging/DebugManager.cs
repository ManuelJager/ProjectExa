using Exa.Input;
using UCommandConsole;
using UnityEngine;
using UnityEngine.InputSystem;
using static Exa.Input.GameControls;

namespace Exa.Debugging
{
    public class DebugManager : MonoBehaviour, IDebugActions
    {
        private GameControls gameControls;
        private Console console;

        public void Awake()
        {
            console = Systems.UI.console;
            gameControls = new GameControls();
            gameControls.Debug.SetCallbacks(this);
        }

        public void OnEnable()
        {
            gameControls.Enable();
        }

        public void OnDisable()
        {
            gameControls.Disable();
        }

        public void OnToggleConsole(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            var consoleGO = console.gameObject;
            consoleGO.SetActive(!consoleGO.activeSelf);
        }
    }
}