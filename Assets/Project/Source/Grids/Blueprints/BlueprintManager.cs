using Exa.IO;
using System.IO;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    public class BlueprintManager : MonoBehaviour, ISaveable
    {
        [HideInInspector] public ObservableBlueprintCollection observableUserBlueprints = new ObservableBlueprintCollection();
        public BlueprintTypes blueprintTypes;

        [SerializeField] private bool loadOnEnable;
        [SerializeField] private bool saveOnDisable;
        private string userBlueprintsDirectory;

        private void Awake()
        {
            userBlueprintsDirectory = Path.Combine(Application.persistentDataPath, "userBlueprints").Replace("/", "\\");
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
            CollectionUtils.LoadToCollectionFromDirectory(observableUserBlueprints, userBlueprintsDirectory);
        }

        [ContextMenu("Save")]
        public void Save()
        {
            CollectionUtils.SaveCollectionToDirectory(observableUserBlueprints, userBlueprintsDirectory, true);
        }
    }
}