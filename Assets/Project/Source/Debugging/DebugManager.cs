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
        DebuggingAI = 1 << 1
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

        // TODO: this is broken. Fuck this console
        public void SetFlag(string debugName)
        {
            if (Enum.TryParse<DebugMode>(debugName, out var mode))
            {
                Systems.Debug.DebugMode ^= mode;
                var set = Systems.Debug.DebugMode.HasFlag(mode);
                var enabledMessage = set ? "enabled" : "disabled";
                console.output.Print($"Debug {mode} is now {enabledMessage}", OutputColor.accent);
            }
            else
            {
                var names = Enum.GetNames(typeof(DebugMode));
                console.output.Print($"Debug name {debugName} is not valid", OutputColor.warning);
                console.output.Print($"Availible flags: {string.Join(" | ", names)}", OutputColor.warning);
            }
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