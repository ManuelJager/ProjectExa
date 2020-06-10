using Exa.Debug.DebugConsole;
using Exa.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using static Exa.Input.GameControls;

namespace Exa.Debug
{
    public class DebugController : MonoBehaviour, IDebugActions
    {
        [SerializeField] private DebugConsoleController consoleController;

        private GameControls gameControls;

        public void Awake()
        {
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

            consoleController.ToggleActive();
        }
    }
}