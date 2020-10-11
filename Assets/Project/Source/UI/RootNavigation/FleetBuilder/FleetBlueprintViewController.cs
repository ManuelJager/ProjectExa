using System.Collections.Generic;
using System.Linq;
using Exa.Bindings;
using Exa.Grids.Blueprints;
using UnityEngine;
using UnityEngine.Events;

namespace Exa.UI
{
    public class FleetBlueprintViewController : ViewController<FleetBlueprintView, BlueprintContainer, Blueprint>
    {
        [SerializeField] private GameObject tabPrefab;
        private Dictionary<BlueprintType, BlueprintTypeTabContent> tabsByType = new Dictionary<BlueprintType, BlueprintTypeTabContent>();
        private UnityAction<BlueprintContainer> viewButtonCallback;

        private UnityEvent<BlueprintContainer> selectedViewRemoved = new UnityEvent<BlueprintContainer>();

        public void Init(
            UnityAction<BlueprintContainer> viewButtonCallback,
            UnityAction<BlueprintContainer> viewSelectionCallback,
            IObservableCollection<BlueprintContainer> collection)
        {
            this.viewButtonCallback = viewButtonCallback;

            selectedViewRemoved.AddListener(viewSelectionCallback);
            Source = collection;
        }

        public override void OnAdd(BlueprintContainer observer)
        {
            var tab = tabsByType[observer.Data.BlueprintType];

            base.OnAdd(observer, tab.container);

            var view = GetView(observer);
            view.NormalContainer = tab.container;
            view.button.onClick.AddListener(() => viewButtonCallback?.Invoke(observer));
        }

        public override void OnRemove(BlueprintContainer observer)
        {
            if (GetView(observer).Selected)
                selectedViewRemoved.Invoke(observer);
            
            base.OnRemove(observer);
        }

        public BlueprintTypeTabContent CreateTab(BlueprintType blueprintType)
        {
            var tab = Instantiate(tabPrefab, transform)
                .GetComponent<BlueprintTypeTabContent>();
            tabsByType.Add(blueprintType, tab);
            tab.gameObject.SetActive(false);
            return tab;
        }
    }
}