using Exa.Generics;
using System.Collections.Generic;

namespace Exa.Grids.Blueprints
{
    public class BlueprintClasificationSource : IValuesSourceProvider
    {
        public IEnumerable<ValueContext> GetValues()
        {
            var types = GameManager.Instance.blueprintManager.blueprintTypes.typesById.Values;
            foreach (var type in types)
            {
                yield return new ValueContext
                {
                    name = type.displayName,
                    value = type.typeGuid
                };
            }
        }
    }

    public class BlueprintCreationOptionsDescriptor : ModelDescriptor<BlueprintCreationOptions>
    {
        public string Name { get; set; }

        [Source(typeof(BlueprintClasificationSource))]
        public string Class { get; set; }

        public override BlueprintCreationOptions FromDescriptor()
        {
            return new BlueprintCreationOptions(Name, Class);
        }
    }
}