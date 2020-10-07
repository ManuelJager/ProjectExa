using Exa.Input;
using System;
using System.Reflection;
using UCommandConsole;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static Exa.Input.GameControls;

namespace Exa.Debugging
{
    public class DebugManager : MonoBehaviour, IDebugActions
    {
        public static event DebugChangeDelegate DebugChange;

        [SerializeField] private DebugMode _debugMode;
        [SerializeField] private DebugDragger _debugDragger;
        private GameControls _gameControls;
        private UCommandConsole.Console _console;

        public DebugMode DebugMode
        {
            get => _debugMode;
            set => _debugMode = value;
        }

        public void Awake()
        {
            _console = Systems.Ui.console;
            _gameControls = new GameControls();
            _gameControls.Debug.SetCallbacks(this);
        }

        public void OnEnable()
        {
            _gameControls.Enable();
        }

        public void OnDisable()
        {
            _gameControls.Disable();
        }

        public void InvokeChange()
        {
            DebugChange?.Invoke(DebugMode);
        }

        public void OnToggleConsole(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            var consoleGo = _console.gameObject;
            consoleGo.SetActive(!consoleGo.activeSelf);
        }

        public void OnDrag(InputAction.CallbackContext context)
        {
            if (!DebugMode.Dragging.GetEnabled()) return;

            switch (context.phase)
            {
                case InputActionPhase.Started:
                    _debugDragger.OnPress();
                    break;
                case InputActionPhase.Canceled:
                    _debugDragger.OnRelease();
                    break;
                default:
                    break;
            }
        }

        public static void ClearLog()
        {
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
        /// Evaluates wether a debug mode is globally enabled
        /// </summary>
        /// <param name="debugMode"></param>
        /// <returns></returns>
        public static bool GetEnabled(this DebugMode debugMode)
        {
            return (Systems.Debug.DebugMode & debugMode) != 0;
        }

        /// <summary>
        /// Adds the given debug mode bitmask to the current global debug mode
        /// </summary>
        /// <param name="debugMode"></param>
        public static void BinaryAdd(this DebugMode debugMode)
        {
            Systems.Debug.DebugMode |= debugMode;
            Systems.Debug.InvokeChange();
        }

        /// <summary>
        /// To
        /// </summary>
        /// <param name="debugMode"></param>
        public static void Toggle(this DebugMode debugMode)
        {
            Systems.Debug.DebugMode ^= debugMode;
            Systems.Debug.InvokeChange();
        }
    }
}