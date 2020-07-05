namespace Exa.Debugging.Commands.Parser
{
    public abstract class ExposedCommand : Command
    {
        public Tokenizer InputHandle { get; private set; }

        public override void CommandHandle(Console console, Tokenizer tokenizer)
        {
            InputHandle = tokenizer;
            CommandAction();
        }
    }
}