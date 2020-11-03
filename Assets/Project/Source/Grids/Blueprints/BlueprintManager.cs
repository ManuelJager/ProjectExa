using Exa.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Exa.Bindings;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Grids.Blueprints
{
    public class BlueprintManager : MonoBehaviour
    {
        [HideInInspector] public BlueprintContainerCollection userBlueprints = new BlueprintContainerCollection();
        [HideInInspector] public BlueprintContainerCollection defaultBlueprints = new BlueprintContainerCollection();
        [HideInInspector] public CompositeObservableEnumerable<BlueprintContainer> useableBlueprints;
        public BlueprintTypeBag blueprintTypes;

        [SerializeField] private DefaultBlueprintBag defaultBlueprintBag;

        public IEnumerator Init(IProgress<float> progress) {
            var userBlueprintPaths = CollectionUtils
                .GetJsonPathsFromDirectory(DirectoryTree.Blueprints)
                .ToList();

            var defaultBlueprintsList = defaultBlueprintBag.ToList();
            var iterator = 0;
            var blueprintTotal = userBlueprintPaths.Count + defaultBlueprintsList.Count;

            useableBlueprints =
                new CompositeObservableEnumerable<BlueprintContainer>(userBlueprints, defaultBlueprints);

            // Load user defined blueprints
            foreach (var blueprintPath in userBlueprintPaths) {
                try {
                    AddUserBlueprint(blueprintPath);
                }
                catch (Exception e) {
                    Systems.UI.logger.Log($"Error loading blueprint: {e.Message}");
                }

                yield return null;
                iterator++;
                progress.Report((float) iterator / blueprintTotal);
            }


            // Load default blueprints
            foreach (var defaultBlueprint in defaultBlueprintBag) {
                AddDefaultBlueprint(defaultBlueprint);

                yield return null;
                iterator++;
                progress.Report((float) iterator / blueprintTotal);
            }
        }

        public Blueprint GetBlueprint(string name) {
            if (defaultBlueprints.ContainsKey(name))
                return defaultBlueprints[name].Data;

            if (userBlueprints.ContainsKey(name))
                return userBlueprints[name].Data;

            throw new KeyNotFoundException();
        }

        public bool ContainsName(string name) {
            return defaultBlueprints.ContainsKey(name)
                   || userBlueprints.ContainsKey(name);
        }

        private void AddDefaultBlueprint(DefaultBlueprint defaultBlueprint) {
            var blueprint = defaultBlueprint.ToContainer();

            if (ContainsName(blueprint.Data.name))
                throw new ArgumentException("Blueprint named is duplicate");

            defaultBlueprints.Add(blueprint);
        }

        private void AddUserBlueprint(string path) {
            var blueprint = IOUtils.JsonDeserializeFromPath<Blueprint>(path);

            if (blueprint == null) {
                throw new ArgumentNullException("blueprint");
            }

            if (ContainsName(blueprint.name)) {
                throw new ArgumentException($"Blueprint named \"{blueprint.name}\" is duplicate");
            }

            var args = new BlueprintContainerArgs(blueprint) {
                generateBlueprintFileHandle = true,
                generateBlueprintFileName = false
            };

            var observableBlueprint = new BlueprintContainer(args);
            observableBlueprint.BlueprintFileHandle.CurrentPath = path;
            observableBlueprint.LoadThumbnail();
            userBlueprints.Add(observableBlueprint);
        }
    }
}