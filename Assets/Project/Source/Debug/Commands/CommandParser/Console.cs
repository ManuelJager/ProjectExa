using System.Collections.Generic;
using System.IO;

namespace Exa.Debug.Commands.Parser
{
    public delegate void ConsoleOutputDelegate(string output);

    public class Console
    {
        public Dictionary<string, Command> CommandDict { get; }

        /// <summary>
        /// Command output event
        /// </summary>
        public event ConsoleOutputDelegate Output;

        public Console()
        {
            CommandDict = new Dictionary<string, Command>();
            Add(new HelpCommand());
        }

        public void Add(Command command)
        {
            if (CommandDict.ContainsKey(command.Name))
            {
                throw new IncorrectCommandFormatException("Command already added");
            }
            CommandDict.Add(command.Name, command);
        }

        public void InvokeOutput(string value)
        {
            Output?.Invoke(value);
        }

        public void Parse(string input)
        {
            var tokenizer = new Tokenizer(new StringReader(input));

            // Get first word
            var commandAlias = tokenizer.Value;

            // Must be a word
            if (tokenizer.Token != Token.Literal)
            {
                throw new IncorrectCommandFormatException("First input must formatted as a literal value");
            }

            // Command alias must exist
            if (!CommandDict.ContainsKey(commandAlias))
            {
                throw new IncorrectCommandFormatException("First input must a registered command");
            }

            tokenizer.NextToken();

            CommandDict[commandAlias].CommandHandle(this, tokenizer);
        }
    }
}