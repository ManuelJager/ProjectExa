using Exa.Input;
using System.Reflection;
using Exa.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static Exa.Input.GameControls;

#pragma warning disable CS0649

namespace Exa.Debugging
{
    public class DebugManager : MonoBehaviour, IDebugActions
    {
        public static event DebugChangeDelegate DebugChange;

        [SerializeField] private DebugMode debugMode;
        [SerializeField] private DebugMode buildDebugMode;
        [SerializeField] private DebugDragger debugDragger;
        private GameControls gameControls;
        private UCommandConsole.Console console;

        public DebugMode DebugMode => Application.isEditor ? debugMode : buildDebugMode;
        

        public void Awake() {
            console = Systems.UI.console;
            gameControls = new GameControls();
            gameControls.Debug.SetCallbacks(this);
        }

        public void OnEnable() {
            gameControls.Enable();
        }

        public void OnDisable() {
            gameControls.Disable();
        }

        public void InvokeChange() {
            DebugChange?.Invoke(DebugMode);
        }

        public void OnToggleConsole(InputAction.CallbackContext context) {
            if (!context.performed) return;

            var consoleGO = console.gameObject;
            consoleGO.SetActive(!consoleGO.activeSelf);
        }

        public void OnDrag(InputAction.CallbackContext context) {
            if (!DebugMode.Dragging.IsEnabled()) return;

            switch (context.phase) {
                case InputActionPhase.Started:
                    debugDragger.OnPress();
                    break;
                case InputActionPhase.Canceled:
                    debugDragger.OnRelease();
                    break;
                default:
                    break;
            }
        }

        public static void ClearLog() {
#if UNITY_EDITOR
            var assembly = Assembly.GetAssembly(typeof(SceneView));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
#endif
        }
    }

    public static class DebugExtensions
    {
        /// <summary>
        /// Evaluates whether a debug mode is globally enabled
        /// </summary>
        /// <param name="debugMode"></param>
        /// <returns></returns>
        public static bool IsEnabled(this DebugMode debugMode) {
            return (Systems.Debug.DebugMode & debugMode) == debugMode;
        }
    }
}