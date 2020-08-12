using Exa.Input;
using System;
using UCommandConsole;
using UnityEngine;
using UnityEngine.InputSystem;
using static Exa.Input.GameControls;

namespace Exa.Debugging
{
    [Flags]
    [Serializable]
    public enum DebugMode
    {
        Global      = 1 << 0,
        Ships       = 1 << 1,
        Navigation  = 1 << 2
    }

    public class DebugManager : MonoBehaviour, IDebugActions
    {
        [SerializeField] DebugMode debugMode;
        private GameControls gameControls;
        private UCommandConsole.Console console;

        public DebugMode DebugMode
        {
            get => debugMode;
            set => debugMode = value;
        }

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