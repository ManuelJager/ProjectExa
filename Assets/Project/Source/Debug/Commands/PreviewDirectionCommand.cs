using Exa.Debugging.Commands.Parser;
using UnityEngine;

namespace Exa.Debugging.Commands
{
    public class PreviewDirectionCommand : ParameterfulCommand
    {
        [CommandArg] public float x { get; set; }
        [CommandArg] public float y { get; set; }
        [CommandArg] public float z { get; set; }

        public override string Name => "previewDirection";

        public override void CommandAction()
        {
            RuntimePreviewGenerator.PreviewDirection = new Vector3(x, y, z);
        }
    }
}