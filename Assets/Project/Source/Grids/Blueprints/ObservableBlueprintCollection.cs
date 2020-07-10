using Exa.Generics;
using System;

namespace Exa.Grids.Blueprints
{
    [Serializable]
    public class ObservableBlueprintCollection : ObservableDictionary<string, ObservableBlueprint>
    {
        public override bool Remove(ObservableBlueprint item)
        {
            var result = base.Remove(item);

            item.BlueprintFileHandle.Delete();
            item.ThumbnailFileHandle.Delete();

            return result;
        }

        public override void Add(ObservableBlueprint item)
        {
            if (Contains(item))
            {
                UnityEngine.Debug.LogWarning($"Blueprint: {item.Data.name} has already been added");
                return;
            }

            base.Add(item);
        }
    }
}