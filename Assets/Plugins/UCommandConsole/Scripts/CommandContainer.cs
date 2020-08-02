using System;
using System.Collections.Generic;
using UCommandConsole.Models;

namespace UCommandConsole
{
    public class CommandContainer
    {
        private Console host;
        private Stack<Command> commandHistory;
        private Dictionary<string, Func<Command>> commandGetters;

        public int Count => commandGetters.Count;
        public Dictionary<string, CommandContext> commandContext { get; private set; }

        public CommandContainer(Console host)
        {
            this.host = host;
            this.commandHistory = new Stack<Command>();
            this.commandGetters = new Dictionary<string, Func<Command>>(StringComparer.OrdinalIgnoreCase);
            this.commandContext = new Dictionary<string, CommandContext>(StringComparer.OrdinalIgnoreCase);

            AddCommands();
        }

        protected void AddCommands()
        {
            AddGetter(() => new UndoCommand());
            AddGetter(() => new HelpCommand());
        }

        public void AddGetter(Func<Command> commandGetter)
        {
            var command = commandGetter();
            var commandName = command.GetName();

            if (ContainsCommand(commandName))
            {
                throw new Exception($"Attempted to add a command with duplicate name: {commandName}");
            }

            CommandBuilder.Build(command);

            commandGetters[commandName] = commandGetter;
            commandContext[commandName] = command.Context;
        }

        public void AddToHistory(Command command)
        {
            commandHistory.Push(command);
        }

        public Command Undo()
        {
            var command = commandHistory.Pop();
            command.Undo(host);
            return command;
        }

        public Command PopFromHistory()
        {
            return commandHistory.Pop();
        }

        public bool ContainsCommand(string name)
        {
            return commandGetters.ContainsKey(name);
        }

        public Command GetCommand(string name)
        {
            // Create new command
            var command = commandGetters[name]();

            // Assign the stateless context
            command.Context = commandContext[name];
            return command;
        }
    }
}