using Exa.Generics;
using Newtonsoft.Json;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    public class Blueprint : ICloneable<Blueprint>
    {
        public string name;
        public string shipClass;

        [JsonProperty("blocks")] public BlueprintBlocks Blocks { get; private set; }
        [JsonIgnore] public long Mass { get; set; }
        [JsonIgnore] public float PeakPowerGeneration { get; set; }
        [JsonIgnore] public Texture2D Thumbnail { get; set; }

        public static readonly string DEFAULT_BLUEPRINT_NAME = "New blueprint";

        [JsonIgnore]
        public BlueprintType blueprintType
        {
            get => Systems.Blueprints.blueprintTypes.typesById[shipClass];
        }

        public Blueprint(BlueprintOptions options)
        {
            this.name = options.name;
            this.shipClass = options.shipClass;
            this.Blocks = new BlueprintBlocks();
        }

        [JsonConstructor]
        public Blueprint(string name, string shipClass, BlueprintBlocks blocks)
        {
            this.name = name;
            this.shipClass = shipClass;

            this.Blocks = blocks;

            foreach (var block in blocks)
            {
                AddContext(block);
            }
        }

        public void Add(AnchoredBlueprintBlock anchoredBlueprintBlock)
        {
            Blocks.Add(anchoredBlueprintBlock);
            AddContext(anchoredBlueprintBlock);
        }

        public void AddContext(AnchoredBlueprintBlock anchoredBlueprintBlock)
        {
            var context = anchoredBlueprintBlock.BlueprintBlock.RuntimeContext;
            context.DynamicallyAddTotals(this);
        }

        public void Remove(Vector2Int gridPos)
        {
            RemoveContext(gridPos);
            Blocks.Remove(gridPos);
        }

        public void RemoveContext(Vector2Int gridPos)
        {
            var anchoredBlueprintBlock = Blocks.GetMember(gridPos);
            var context = anchoredBlueprintBlock.BlueprintBlock.RuntimeContext;
            context.DynamicallyRemoveTotals(this);
        }

        public void ClearBlocks()
        {
            Blocks = new BlueprintBlocks();
            Mass = 0;
            PeakPowerGeneration = 0f;
            Thumbnail = null;
        }

        public Blueprint Clone()
        {
            return new Blueprint(name, shipClass, Blocks.Clone());
        }
    }
}