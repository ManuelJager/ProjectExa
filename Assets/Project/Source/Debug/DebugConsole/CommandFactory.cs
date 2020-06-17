using CommandEngine.Models;
using Exa.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exa.Debug
{
    /// <summary>
    /// Generates commands from assembly
    /// </summary>
    public static class CommandFactory
    {
        private static IEnumerable<Type> implementations;
        private static IEnumerable<Command> instances;

        /// <summary>
        /// Get Command inheritors in this assembly and create command instances
        /// </summary>
        static CommandFactory()
        {
            implementations = TypeUtils.GetTypeImplementations(typeof(Command));
            instances = implementations
                .Select((type) => Activator.CreateInstance(type) as Command);
        }

        /// <summary>
        /// Add command instances to console
        /// </summary>
        /// <param name="console"></param>
        public static void AddToConsole(CommandEngine.Console console)
        {
            instances.ToList().ForEach((instance) => console.Add(instance));
        }
    }
}