using Exa.IO;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    public class BlueprintManager : MonoBehaviour, ISaveable
    {
        public BlueprintTypes blueprintTypes;
        [HideInInspector] public ObservableBlueprintCollection observableGameBlueprints = new ObservableBlueprintCollection();
        [HideInInspector] public ObservableBlueprintCollection observableUserBlueprints = new ObservableBlueprintCollection();

        private string gameBlueprintsPath;
        private string userBlueprintsPath;

        [SerializeField] private bool loadOnEnable;
        [SerializeField] private bool saveOnDisable;

        public void Awake()
        {
            gameBlueprintsPath = Path.Combine(Application.persistentDataPath, "gameBlueprints.json").Replace("/", "\\");
            userBlueprintsPath = Path.Combine(Application.persistentDataPath, "userBlueprints.json").Replace("/", "\\");
        }

        private void OnEnable()
        {
            if (loadOnEnable) Load();
        }

        private void OnDisable()
        {
            if (saveOnDisable) Save();
        }

        [ContextMenu("Load")]
        // Has a dependency on block factory
        public void Load()
        {
            List<ObservableBlueprint> gameBlueprints;
            List<ObservableBlueprint> userBlueprints;
            IOUtils.TryJsonDeserializeFromPath(gameBlueprintsPath, out gameBlueprints);
            IOUtils.TryJsonDeserializeFromPath(userBlueprintsPath, out userBlueprints);

            if (gameBlueprints != null)
            {
                observableGameBlueprints.AddRange(gameBlueprints);
            }

            if (userBlueprints != null)
            {
                observableUserBlueprints.AddRange(userBlueprints);
            }
        }

        [ContextMenu("Save")]
        public void Save()
        {
            IOUtils.JsonSerializeToPath(gameBlueprintsPath, observableGameBlueprints.collection);
            IOUtils.JsonSerializeToPath(userBlueprintsPath, observableUserBlueprints.collection);
        }
    }
}