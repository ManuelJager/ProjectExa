#pragma warning disable CS0649

using Exa.Utils;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.Debug.DebugConsole
{
    public class DebugConsoleController : MonoBehaviourInstance<DebugConsoleController>
    {
        private DebugConsole console;
        [SerializeField] private GameObject ownerObject;
        [SerializeField] private Text consoleOutput;
        [SerializeField] private InputField consoleInput;

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