using Exa.Bindings;
using System;
using System.Linq;

namespace Exa.Grids.Blueprints
{
    [Serializable]
    public class ObservableBlueprintCollection : ObservableCollection<ObservableBlueprint>
    {
        public override bool Contains(ObservableBlueprint item)
        {
            return this.Any((x) => x.Data.name == item.Data.name);
        }

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

            item.LoadThumbnail();
            base.Add(item);
        }
    }
}