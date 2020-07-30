using UCommandConsole.Attributes;
using UCommandConsole.TypeParsers;

namespace UCommandConsole
{
    public class TestCommand : Command
    {
        public override string GetName() => "test";

        [CommandArgument(CustomParser = typeof(StringLiteralParser))] public string Val { get; set; }

        public override void Execute(Console host)
        {
            host.output.Print($"Val value: {Val}", OutputColor.accent);
        }
    }
}