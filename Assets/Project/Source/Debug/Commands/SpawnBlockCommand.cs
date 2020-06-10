﻿using CommandEngine;
using CommandEngine.Models;
using UnityEngine;

namespace Exa.Debug.Commands
{
    public class SpawnBlockCommand : ParameterfulCommand
    {
        public override string Name => "spawnblock";
        public override string HelpText => "Spawns a block of given id to 0.0";

        [ArgumentDefinition(0, "Id of the block to be spawned")]
        public string blockId { get; set; }

        public override void CommandHandle(Console console, Tokenizer tokenizer)
        {
            base.CommandHandle(console, tokenizer);
            console.InvokeOutput($"spawned {blockId}");
        }

        public override void CommandAction()
        {
            var prefab = GameManager.Instance.blockFactory.GetBlock(blockId);
            GameObject.Instantiate(prefab);
        }
    }
}