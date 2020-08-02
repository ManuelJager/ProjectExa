using Exa.Generics;
using Exa.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    public class BlueprintManager : MonoBehaviour
    {
        [HideInInspector] public ObservableBlueprintCollection observableUserBlueprints = new ObservableBlueprintCollection();
        [HideInInspector] public ObservableDictionary<string, ObservableBlueprint> observableDefaultBlueprints = new ObservableDictionary<string, ObservableBlueprint>();
        public BlueprintTypeBag blueprintTypes;

        [SerializeField] private DefaultBlueprintBag defaultBlueprintBag;

        public IEnumerator StartUp(IProgress<float> progress)
        {
            var userBlueprintPaths = CollectionUtils
                .GetJsonPathsFromDirectory(IOUtils.GetPath("blueprints"))
                .ToList();
            var defaultBlueprints = defaultBlueprintBag.ToList();
            var iterator = 0;
            var blueprintTotal = userBlueprintPaths.Count + defaultBlueprints.Count;

            // Load default blueprints
            foreach (var defaultBlueprint in defaultBlueprintBag)
            {
                AddDefaultBlueprint(defaultBlueprint);

                yield return null;
                iterator++;
                progress.Report((float)iterator / blueprintTotal);
            }

            // Load user defined blueprints
            foreach (var blueprintPath in userBlueprintPaths)
            {
                var blueprint = IOUtils.JsonDeserializeFromPath<Blueprint>(blueprintPath);
                AddUserBlueprint(blueprint);

                yield return null;
                iterator++;
                progress.Report((float)iterator / blueprintTotal);
            }
        }

        // Has a dependency on block factory
        [ContextMenu("Load")]
        public void Load()
        {
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