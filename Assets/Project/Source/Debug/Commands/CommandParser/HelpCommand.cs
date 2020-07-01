using System.Collections.Generic;

namespace Exa.Debug.Commands.Parser
{
    internal class HelpCommand : ExposedCommand
    {
        public override string Name => "help";

        public override void CommandHandle(Console console, Tokenizer tokenizer)
        {
            // Handle no parameter
            if (tokenizer.Token == Token.EOF)
            {
                console.InvokeOutput("Availible commands: \n\n" + string.Join("\n", console.CommandDict.Keys));
            }
            // handle first parameter
            else if (tokenizer.Token == Token.Literal)
            {
                HelpHandleFirstParameter(console, tokenizer);
            }
            // handle invalid parameter
            else throw new IncorrectCommandFormatException("Expected a command name");
        }

        private void HelpHandleFirstParameter(Console console, Tokenizer tokenizer)
        {
            var commandName = tokenizer.Value;
            if (!console.CommandDict.ContainsKey(commandName))
            {
                // handle non existant command
                throw new IncorrectCommandFormatException($"Command \"{commandName}\" doesn't exist");
            }

            // Get command
            var arg0 = tokenizer.Value;
            var command = console.CommandDict[arg0];

            // Go to second parameter
            tokenizer.NextToken();

            if (tokenizer.Token == Token.EOF)
            {
                HelpHandleCommandOutput(console, command, commandName);
            }
            else if (tokenizer.Token == Token.Literal)
            {
                HelpHandleSecondParameter(console, tokenizer, command);
            }
            // handle incorrect command property format
            else throw new IncorrectCommandFormatException("Expected a command property");
        }

        private void HelpHandleCommandOutput(Console console, Command command, string commandName)
        {
            var lines = new List<string>
            {
                $"{commandName}: {command.HelpText}"
            };

            // if the command contains properties, print them out
            if (command.GetType().IsSubclassOf(typeof(ParameterfulCommand)))
            {
                var propertiesHelpText = ((ParameterfulCommand)command).Context.propertiesHelpText;

                lines.Add("Properties: ");

                foreach (var keyValuePair in propertiesHelpText)
                {
                    lines.Add($"\t{keyValuePair.Key}: {keyValuePair.Value}");
                }
            }

            console.InvokeOutput(string.Join("\n", lines));
        }

        private void HelpHandleSecondParameter(Console console, Tokenizer tokenizer, Command command)
        {
            if (command.GetType().IsSubclassOf(typeof(ParameterfulCommand)))
            {
                var propertiesHelpText = ((ParameterfulCommand)command).Context.propertiesHelpText;
                var arg1 = tokenizer.Value;

                if (propertiesHelpText.ContainsKey(arg1))
                {
                    var helpText = propertiesHelpText[arg1];
                    // log command property
                    console.InvokeOutput($"{arg1}: {helpText}");
                }
                // handle property not found
                else throw new IncorrectCommandFormatException($"Command doesn't contain property \"{arg1}\"");
            }
            // handle incorrect command type
            else throw new IncorrectCommandFormatException("Command contains no defined properties");
        }

        public override void CommandAction()
        {
        }
    }
}