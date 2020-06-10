using CommandEngine.Models;
using Exa.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exa.Debug.Commands
{
    public static class CommandFactory
    {
        private static IEnumerable<Type> implementations;
        private static IEnumerable<Command> instances;

        static CommandFactory()
        {
            implementations = TypeUtils.GetTypeImplementations(typeof(Command));
            instances = implementations
                .ToList()
                .Select((type) => Activator.CreateInstance(type) as Command);
        }

        public static void AddToConsole(CommandEngine.Console console)
        {
            instances.ToList().ForEach((instance) => console.Add(instance));
        }
    }
}