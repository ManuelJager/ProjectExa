#pragma warning disable CS0649

using Exa.Utils;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.Debug
{
    /// <summary>
    /// Console controller
    /// </summary>
    public class DebugConsoleController : MonoSingleton<DebugConsoleController>
    {
        [SerializeField] private GameObject ownerObject;
        [SerializeField] private Text consoleOutput;
        [SerializeField] private InputField consoleInput;
        private DebugConsole console;

        public string ConsoleOutputText
        {
            get => consoleOutput.text;
            set => consoleOutput.text = value;
        }

        public new void Awake()
        {
            base.Awake();
            console = new DebugConsole();
            console.Output += OutputToConsole;
            consoleInput.onEndEdit.AddListener(OnInputEndEdit);
        }

        public void OutputToConsole(string output)
        {
            OutputToConsole(output, true);
        }

        public void OutputToConsole(string output, bool format)
        {
            consoleOutput.text += format ? FormatOutput(output) : output;
        }

        public void ToggleActive()
        {
            var active = ownerObject.activeSelf;
            ownerObject.SetActive(!active);
            if (!active)
            {
                consoleInput.ActivateInputField();
            }
        }

        private string FormatOutput(string output)
        {
            return $"\n{output}";
        }

        private void BeginCaptureInput()
        {
            OutputToConsole("-----------------\n", false);
        }

        private void StopCaptureInput()
        {
            OutputToConsole("\n", false);
        }

        /// <summary>
        /// On console input end edit
        /// </summary>
        /// <param name="input"></param>
        private void OnInputEndEdit(string input)
        {
            if (input == "") return;

            BeginCaptureInput();

            try
            {
                OutputToConsole($"{input} >", false);
                console.Parse(input);
                consoleInput.text = "";
            }
            catch (Exception e)
            {
                OutputToConsole(e.Message);
                consoleInput.text = "";
            }

            StopCaptureInput();

            consoleInput.ActivateInputField();
        }
    }
}