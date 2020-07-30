using System;
using System.Collections.Generic;
using UCommandConsole.Exceptions;
using UCommandConsole.Models;
using UCommandConsole.TypeParsers;
using UnityEngine;

namespace UCommandConsole
{
    // TODO: Add named arguments
    // TODO: Add command output as command
    public class Console : MonoBehaviour
    {
        [Header("References")]
        public ConsoleOutput output;
        public CommandContainer container;
        public CommandParserContext parserContext;

        private void Awake()
        {
            container = new CommandContainer(this);
        }

        public void OnSubmit(string input)
        {
            output.BeginPrint($"Command output: {input}");

            void HandleException(Exception e, string message)
            {
                output.Print(message, OutputColor.error);
                output.DumpExceptionLogRecursively(e);
            }

            // Run command
            try
            {
                output.Print($"> {input}");
                Run(input);
            }
            catch (InputFormatException e)
            {
                HandleException(e, "Unhandled exception encoutered while parsing command string");
            }
            catch (Exception e)
            {
                HandleException(e, "Unhandled exception encoutered while executing command");
            }

            output.EndPrint();
        }

        public bool Run(string commandString)
        {
            var parser = new CommandParser(commandString, parserContext);
            var commandName = parser.AsValue(new StringLiteralParser());

            // Select command
            if (!container.ContainsCommand(commandName))
            {
                output.Print($"Command: \"{commandName}\" doesn't exist", OutputColor.warning);
                return false;
            }

            var command = container.GetCommand(commandName);
            return Run(command, parser);
        }

        protected bool Run(Command command, CommandParser parser)
        {
            command.CurrentParser = parser;

            // Parameterize
            try
            {
                command.Parameterize();
            }
            catch (ParameterizationException e)
            {
                output.Print(e.Message, OutputColor.warning);
                return false;
            }
            catch (Exception e)
            {
                throw new InputFormatException($"Unhandled error while parsing parameters on command: \"{name}\"", e);
            }

            // Execute
            try
            {
                command.Execute(this);
            }
            catch
            {
                output.Print($"Error executing command", OutputColor.error);
                return false;
            }

            // Add command history to queue
            if (!command.Context.info.ignoresHistory)
            {
                container.AddToHistory(command);
            }

            command.CurrentParser = null;
            return true;
        }
    }
}