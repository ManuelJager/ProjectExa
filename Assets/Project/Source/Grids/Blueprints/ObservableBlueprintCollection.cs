using Exa.Bindings;
using System;
using System.Linq;

namespace Exa.Grids.Blueprints
{
    [Serializable]
    public class ObservableBlueprintCollection : ObservableCollection<ObservableBlueprint>
    {
        public override void Add(ObservableBlueprint item)
        {
            if (this.Any((x) => x.Data.name == item.Data.name))
            {
                UnityEngine.Debug.LogWarning($"Blueprint: {item.Data.name} has already been added");
                return;
            }

            base.Add(item);
        }
    }
}