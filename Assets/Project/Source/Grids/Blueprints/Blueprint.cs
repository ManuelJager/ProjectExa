using Exa.Generics;
using Exa.Grids.Blocks.Components;
using Exa.Utils;
using Newtonsoft.Json;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    public class Blueprint : ICloneable<Blueprint>
    {
        private BlueprintBlocks blocks;
        public string name;
        public string shipClass;


        [JsonIgnore] public BlueprintBlocks Blocks => blocks;
        [JsonIgnore] public long Mass { get; private set; }
        [JsonIgnore] public float PeakPowerGeneration { get; private set; }

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

            blocks.anchoredBlueprintBlocks.ForEach((block) => AddContext(block));
        }

        public void Add(AnchoredBlueprintBlock anchoredBlueprintBlock)
        {
            blocks.Add(anchoredBlueprintBlock);
            AddContext(anchoredBlueprintBlock);
        }

        public void AddContext(AnchoredBlueprintBlock anchoredBlueprintBlock)
        {
            var context = anchoredBlueprintBlock.blueprintBlock.RuntimeContext;

            // Add mass to grid
            TypeUtils.OnAssignableFrom<IPhysicalBlockTemplateComponent>(context, (component) =>
            {
                Mass += component.PhysicalBlockTemplateComponent.Mass;
            });

            // Add peak consumption to grid
            TypeUtils.OnAssignableFrom<IPowerGeneratorBlockTemplateComponent>(context, (component) =>
            {
                PeakPowerGeneration += component.PowerGeneratorBlockTemplateComponent.PeakGeneration;
            });
        }

        public void Remove(Vector2Int gridPos)
        {
            blocks.Remove(gridPos);
            RemoveContext(gridPos);
        }

        public void RemoveContext(Vector2Int gridPos)
        {
            var anchoredBlueprintBlock = blocks.GetAnchoredBlockAtGridPos(gridPos);
            var context = anchoredBlueprintBlock.blueprintBlock.RuntimeContext;

            // Remove mass from grid
            TypeUtils.OnAssignableFrom<IPhysicalBlockTemplateComponent>(context, (component) =>
            {
                Mass -= component.PhysicalBlockTemplateComponent.Mass;
            });

            // Remove peak consumption from grid
            TypeUtils.OnAssignableFrom<IPowerGeneratorBlockTemplateComponent>(context, (component) =>
            {
                PeakPowerGeneration -= component.PowerGeneratorBlockTemplateComponent.PeakGeneration;
            });
        }

        public void ClearBlocks()
        {
            blocks = new BlueprintBlocks();
            Mass = 0;
            PeakPowerGeneration = 0f;
        }

        public Blueprint Clone()
        {
            return new Blueprint(name, shipClass, blocks.Clone());
        }
    }
}