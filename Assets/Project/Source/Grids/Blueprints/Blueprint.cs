using Exa.Generics;
using Exa.Utils;
using Newtonsoft.Json;
using System.Text;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    public class Blueprint : ICloneable<Blueprint>
    {
        public string name;
        public string shipClass;

        [JsonProperty("blocks")] public BlueprintBlocks Blocks { get; private set; }
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
        }

        public void Add(AnchoredBlueprintBlock anchoredBlueprintBlock)
        {
            Blocks.Add(anchoredBlueprintBlock);
        }

        public void Remove(Vector2Int gridPos)
        {
            Blocks.Remove(gridPos);
        }

        public void ClearBlocks()
        {
            Blocks = new BlueprintBlocks();
            Thumbnail = null;
        }

        public Blueprint Clone()
        {
            return new Blueprint(name, shipClass, Blocks.Clone());
        }

        public string ToString(int tabs = 0)
        {
            var sb = new StringBuilder();
            sb.AppendLineIndented($"Name: {name}", tabs);
            sb.AppendLineIndented($"Class: {shipClass}", tabs);
            sb.AppendLineIndented($"Size: {Blocks.Size.Value}", tabs);
            sb.AppendLineIndented($"Blocks (Count: {Blocks.GetMemberCount()}):", tabs);
            sb.AppendLine(Blocks.Totals.ToString(tabs + 1));
            return sb.ToString();
        }
    }
}