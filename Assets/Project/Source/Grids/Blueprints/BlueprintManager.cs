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
        [HideInInspector] public BlueprintContainerCollection observableUserBlueprints = new BlueprintContainerCollection();
        [HideInInspector] public BlueprintContainerCollection observableDefaultBlueprints = new BlueprintContainerCollection();
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
                observableDefaultBlueprints.Add(defaultBlueprint.ToContainer());

                yield return null;
                iterator++;
                progress.Report((float)iterator / blueprintTotal);
            }

            // Load user defined blueprints
            foreach (var blueprintPath in userBlueprintPaths)
            {
                var blueprint = IOUtils.JsonDeserializeFromPath<Blueprint>(blueprintPath);
                AddUserBlueprint(blueprint, blueprintPath);

                yield return null;
                iterator++;
                progress.Report((float)iterator / blueprintTotal);
            }
        }

        public BlueprintContainer GetBlueprint(string name)
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

        private void AddUserBlueprint(Blueprint blueprint, string path)
        {
            if (blueprint == null || BlueprintNameExists(blueprint.name)) return;

            var args = new BlueprintContainerArgs(blueprint)
            {
                generateBlueprintFileHandle = true,
                generateBlueprintFileName = false
            };

            var observableBlueprint = new BlueprintContainer(args);
            observableBlueprint.BlueprintFileHandle.CurrentPath = path;
            observableBlueprint.LoadThumbnail();
            observableUserBlueprints.Add(observableBlueprint);
        }
    }
}