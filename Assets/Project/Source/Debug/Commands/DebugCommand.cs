using Exa.Debugging.Commands.Parser;

namespace Exa.Debugging.Commands
{
    public delegate void DebugChangeDelegate(bool state);

    public class DebugCommand : ParameterlessCommand
    {
        public override string Name => "debug";
        public override string HelpText => "Toggle dev debug";

        private bool debugEnabled = false;

        public static event DebugChangeDelegate DebugChange;

        public override void CommandHandle(Console console, Tokenizer tokenizer)
        {
            base.CommandHandle(console, tokenizer);
            console.InvokeOutput(debugEnabled ? "debug is enabled" : "debug is disabled");
        }

        public override void CommandAction()
        {
            debugEnabled = !debugEnabled;
            DebugChange?.Invoke(debugEnabled);
        }
    }
}