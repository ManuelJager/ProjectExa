using UCommandConsole;

namespace Exa.Debugging.Commands
{
    public delegate void DebugChangeDelegate(bool state);

    public class ToggleDebugCommand : Command
    {
        private static bool debugEnabled = false;
        public static event DebugChangeDelegate DebugChange;

        public override string GetName() => "debug";

        public override void Execute(Console host)
        {
            debugEnabled = !debugEnabled;
            DebugChange?.Invoke(debugEnabled);
            var message = debugEnabled ? "debug is enabled" : "debug is disabled";
            host.output.Print(message, OutputColor.accent);
        }
    }
}