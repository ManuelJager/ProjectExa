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

        [JsonProperty("blocks")] public BlueprintGrid Grid { get; private set; }
        [JsonIgnore] public Texture2D Thumbnail { get; set; }

        [JsonIgnore] public BlueprintType BlueprintType => Systems.Blueprints.blueprintTypes.typesById[shipClass];

        public Blueprint(BlueprintOptions options) {
            this.name = options.name;
            this.shipClass = options.shipClass;
            this.Grid = new BlueprintGrid();
        }

        [JsonConstructor]
        public Blueprint(string name, BlueprintTypeGuid shipClass, BlueprintGrid grid) {
            this.name = name;
            this.shipClass = shipClass;
            this.Grid = grid;
        }

        public void Add(ABpBlock aBpBlock) {
            Grid.Add(aBpBlock);
        }

        public void Remove(Vector2Int gridPos) {
            Grid.Remove(gridPos);
        }

        public void ClearBlocks() {
            Grid = new BlueprintGrid();
            Thumbnail = null;
        }

        public Blueprint Clone() {
            return new Blueprint(name, shipClass, Grid.Clone());
        }

        public IEnumerable<ITooltipComponent> GetDebugTooltipComponents() => new ITooltipComponent[] {
            new TooltipText($"Name: {name}"),
            new TooltipText($"Class: {shipClass}"),
            new TooltipText($"Size: {(Vector2Int)Grid.Size}"),
            new TooltipText($"Blocks (Count: {Grid.GetMemberCount()}):"),
            new TooltipGroup(Grid.GetTotals().GetDebugTooltipComponents(), 1)
        };
    }
}