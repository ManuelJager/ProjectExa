using Exa.Bindings;
using Exa.Generics;
using Exa.IO;
using Exa.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    public class BlueprintManager : MonoBehaviour
    {
        [HideInInspector] public ObservableBlueprintCollection observableUserBlueprints = new ObservableBlueprintCollection();
        [HideInInspector] public ObservableDictionary<string, ObservableBlueprint> observableDefaultBlueprints = new ObservableDictionary<string, ObservableBlueprint>();
        public BlueprintTypes blueprintTypes;

        [SerializeField] private bool loadOnEnable;

        public void StartUp()
        {
            foreach (var defaultBlueprint in MiscUtils.GetAllInstances<DefaultBlueprint>())
            {
                AddDefaultBlueprint(defaultBlueprint);
            }

            if (loadOnEnable) Load();
        }

        // Has a dependency on block factory
        [ContextMenu("Load")]
        public void Load()
        {
            var path = IOUtils.GetPath("blueprints");
            CollectionUtils.LoadJsonCollectionFromDirectory<Blueprint>(path, AddUserBlueprint);
        }

        public ObservableBlueprint GetBlueprint(string name)
        {
            if (observableDefaultBlueprints.ContainsKey(name))
            {
                return observableDefaultBlueprints[name];
            }

            if (observableUserBlueprints.ContainsKey(name))
            {
                return observableUserBlueprints[name];
            }

            throw new KeyNotFoundException();
        }

        public bool BlueprintNameExists(string name)
        {
            return observableDefaultBlueprints.ContainsKey(name)
                || observableUserBlueprints.ContainsKey(name);
        }

        private void AddDefaultBlueprint(DefaultBlueprint defaultBlueprint)
        {
            var blueprint = IOUtils.JsonDeserializeWithSettings<Blueprint>(defaultBlueprint.blueprintJson, SerializationMode.readable);
            var observableBlueprint = new ObservableBlueprint(blueprint, false);
            observableDefaultBlueprints.Add(observableBlueprint);
        }

        private void AddUserBlueprint(Blueprint blueprint)
        {
            if (blueprint == null || BlueprintNameExists(blueprint.name)) return;

            var observableBlueprint = new ObservableBlueprint(blueprint);
            observableBlueprint.LoadThumbnail();
            observableUserBlueprints.Add(observableBlueprint);
        }
    }
}