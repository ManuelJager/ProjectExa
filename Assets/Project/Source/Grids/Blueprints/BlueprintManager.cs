using Exa.IO;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    public class BlueprintManager : MonoBehaviour
    {
        [HideInInspector] public ObservableBlueprintCollection observableUserBlueprints = new ObservableBlueprintCollection();
        public BlueprintTypes blueprintTypes;

        [SerializeField] private bool loadOnEnable;

        private void OnEnable()
        {
            if (loadOnEnable) Load();
        }

        // Has a dependency on block factory
        [ContextMenu("Load")]
        public void Load()
        {
            var blueprintPath = IOUtils.CombinePathWithDataPath(RelativeDir.USER_BLUEPRINTS);
            CollectionUtils.LoadJsonCollectionFromDirectory(observableUserBlueprints, blueprintPath);
        }
    }
}