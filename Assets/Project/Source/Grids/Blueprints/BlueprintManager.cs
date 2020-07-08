using Exa.Debugging;
using Exa.Generics;
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
            CollectionUtils.LoadJsonCollectionFromDirectory<ObservableBlueprint>(IOUtils.GetPath("blueprints"), Add);
        }

        private void Add(ObservableBlueprint observableBlueprint)
        {
            if (observableBlueprint != null)
            {
                observableUserBlueprints.Add(observableBlueprint);
            }
            else
            {
                var exception = new UserException("Error parsing a blueprint from disk. Please avoid editing blueprints directly");
                UnityLoggerInterceptor.Instance.LogUserException(exception);
            }
        }
    }
}