using UCommandConsole;

namespace Exa.Debugging.Commands
{
    public class ToggleDebugCommand : Command
    {
        public override string GetName() => "tgl-debug";

        public override void Execute(Console host)
        {
            Systems.DebugIsEnabled = !Systems.DebugIsEnabled;
            var message = Systems.DebugIsEnabled ? "debug is enabled" : "debug is disabled";
            host.output.Print(message, OutputColor.accent);
        }
    }
}