using Exa.Debugging.Commands.Parser;
using System;

namespace Exa.Debugging.Commands
{
    public class SpawnCommand : ParameterfulCommand
    {
        public override string Name => "spawn-ship";

        [CommandArg(0)]
        public string BlueprintName { get; set; }

        public override void CommandAction()
        {
            throw new NotImplementedException();
        }
    }
}