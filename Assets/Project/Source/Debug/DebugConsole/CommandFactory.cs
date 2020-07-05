using Exa.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Debugging.Commands.Parser;

namespace Exa.Debugging
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
            implementations = TypeUtils.GetTypeImplementations(typeof(Command))
                .Where(IsPublic);

            instances = implementations
                .Select((type) => Activator.CreateInstance(type) as Command);
        }

        /// <summary>
        /// Add command instances to console
        /// </summary>
        /// <param name="console"></param>
        public static void AddToConsole(Commands.Parser.Console console)
        {
            instances.ToList().ForEach((instance) => console.Add(instance));
        }

        private static bool IsPublic(Type t)
        {
            return
                t.IsVisible
                && t.IsPublic
                && !t.IsNotPublic
                && !t.IsNested
                && !t.IsNestedPublic
                && !t.IsNestedFamily
                && !t.IsNestedPrivate
                && !t.IsNestedAssembly
                && !t.IsNestedFamORAssem
                && !t.IsNestedFamANDAssem;
        }
    }
}