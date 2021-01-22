using Exa.Generics;
using Exa.UI.Tooltips;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    public class Blueprint : ICloneable<Blueprint>
    {
        public static readonly string DEFAULT_BLUEPRINT_NAME = "New blueprint";

        public string name;
        public BlueprintTypeGuid shipClass;

        [JsonProperty("blocks")] public BlueprintBlocks Blocks { get; private set; }
        [JsonIgnore] public Texture2D Thumbnail { get; set; }

        [JsonIgnore] public BlueprintType BlueprintType => Systems.Blueprints.blueprintTypes.typesById[shipClass];

        public Blueprint(BlueprintOptions options) {
            this.name = options.name;
            this.shipClass = options.shipClass;
            this.Blocks = new BlueprintBlocks();
        }

        [JsonConstructor]
        public Blueprint(string name, BlueprintTypeGuid shipClass, BlueprintBlocks blocks) {
            this.name = name;
            this.shipClass = shipClass;
            this.Blocks = blocks;
        }

        public void Add(ABpBlock aBpBlock) {
            Blocks.Add(aBpBlock);
        }

        public void Remove(Vector2Int gridPos) {
            Blocks.Remove(gridPos);
        }

        public void ClearBlocks() {
            Blocks = new BlueprintBlocks();
            Thumbnail = null;
        }

        public Blueprint Clone() {
            return new Blueprint(name, shipClass, Blocks.Clone());
        }

        public IEnumerable<ITooltipComponent> GetDebugTooltipComponents() => new ITooltipComponent[] {
            new TooltipText($"Name: {name}"),
            new TooltipText($"Class: {shipClass}"),
            new TooltipText($"Size: {(Vector2Int)Blocks.Size}"),
            new TooltipText($"Blocks (Count: {Blocks.GetMemberCount()}):"),
            new TooltipGroup(Blocks.Totals.GetDebugTooltipComponents(), 1)
        };
    }
}