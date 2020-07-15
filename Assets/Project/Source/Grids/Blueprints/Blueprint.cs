using Exa.Generics;
using Exa.Grids.Blocks.Components;
using Exa.Utils;
using Newtonsoft.Json;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    public class Blueprint : ICloneable<Blueprint>
    {
        public string name;
        public string shipClass;

        [JsonProperty("blocks")] public BlueprintBlocks Blocks { get; private set; }
        [JsonIgnore] public long Mass { get; private set; }
        [JsonIgnore] public float PeakPowerGeneration { get; private set; }
        [JsonIgnore] public Texture2D Thumbnail { get; set; }

        public static readonly string DEFAULT_BLUEPRINT_NAME = "New blueprint";

        [JsonIgnore]
        public BlueprintType blueprintType
        {
            get => MainManager.Instance.blueprintManager.blueprintTypes.typesById[shipClass];
        }

        public Blueprint(BlueprintCreationOptions options)
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

            if (Blocks != null)
            {
                blocks.AnchoredBlueprintBlocks.ForEach((block) => AddContext(block));
            }
        }

        public void Add(AnchoredBlueprintBlock anchoredBlueprintBlock)
        {
            Blocks.Add(anchoredBlueprintBlock);
            AddContext(anchoredBlueprintBlock);
        }

        public void AddContext(AnchoredBlueprintBlock anchoredBlueprintBlock)
        {
            var context = anchoredBlueprintBlock.blueprintBlock.RuntimeContext;

            /*
            // Add mass to grid
            context.OnAssignableFrom<IPhysicalBlockTemplateComponent>((component) =>
            {
                Mass += component.PhysicalBlockTemplateComponent.Mass;
            });

            // Add peak consumption to grid
            context.OnAssignableFrom<IPowerGeneratorBlockTemplateComponent>((component) =>
            {
                PeakPowerGeneration += component.PowerGeneratorBlockTemplateComponent.PeakGeneration;
            });
            */
        }

        public void Remove(Vector2Int gridPos)
        {
            RemoveContext(gridPos);
            Blocks.Remove(gridPos);
        }

        public void RemoveContext(Vector2Int gridPos)
        {
            var anchoredBlueprintBlock = Blocks.GetAnchoredBlockAtGridPos(gridPos);
            var context = anchoredBlueprintBlock.blueprintBlock.RuntimeContext;

            /*
            // Remove mass from grid
            context.OnAssignableFrom<IPhysicalBlockTemplateComponent>((component) =>
            {
                Mass -= component.PhysicalBlockTemplateComponent.Mass;
            });

            // Remove peak consumption from grid
            context.OnAssignableFrom<IPowerGeneratorBlockTemplateComponent>((component) =>
            {
                PeakPowerGeneration -= component.PowerGeneratorBlockTemplateComponent.PeakGeneration;
            });
            */
        }

        public void ClearBlocks()
        {
            Blocks = new BlueprintBlocks();

            Mass = 0;
            PeakPowerGeneration = 0f;
        }

        public Blueprint Clone()
        {
            return new Blueprint(name, shipClass, Blocks.Clone());
        }
    }
}