using Exa.Generics;
using Newtonsoft.Json;
using System;

namespace Exa.Grids.Blueprints
{
    public class Blueprint : ICloneable<Blueprint>
    {
        public string name;
        public string shipClass;
        public BlueprintBlocks blocks;

        public static readonly string DEFAULT_BLUEPRINT_NAME = "New blueprint";

        [JsonIgnore]
        public BlueprintType blueprintType
        {
            get => GameManager.Instance.blueprintManager.blueprintTypes.typesById[shipClass];
        }

        public Blueprint(BlueprintCreationOptions options)
        {
            this.name = options.name;
            this.shipClass = options.shipClass;
            this.blocks = new BlueprintBlocks();
        }

        [JsonConstructor]
        public Blueprint(string name, string shipClass, BlueprintBlocks blocks)
        {
            this.name = name;
            this.shipClass = shipClass;
            this.blocks = blocks;
        }

        public Blueprint Clone()
        {
            return new Blueprint(name, shipClass, blocks.Clone());
        }
    }
}