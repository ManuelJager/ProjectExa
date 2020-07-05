using Exa.IO;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    public class BlueprintManager : MonoBehaviour, ISaveable
    {
        [HideInInspector] public ObservableBlueprintCollection observableUserBlueprints = new ObservableBlueprintCollection();
        public BlueprintTypes blueprintTypes;

        [SerializeField] private bool loadOnEnable;
        [SerializeField] private bool saveOnDisable;

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
            var path = IOUtils.CombinePathWithDataPath("userBlueprints");
            CollectionUtils.LoadToCollectionFromDirectory(observableUserBlueprints, path);
        }

        [ContextMenu("Save")]
        public void Save()
        {
            var path = IOUtils.CombinePathWithDataPath("userBlueprints");
            CollectionUtils.SaveCollectionToDirectory(observableUserBlueprints, path, true);
        }
    }
}