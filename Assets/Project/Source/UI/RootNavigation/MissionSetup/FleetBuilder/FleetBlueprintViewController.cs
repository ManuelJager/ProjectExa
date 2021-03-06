﻿using System.Collections.Generic;
using Exa.Bindings;
using Exa.Grids.Blueprints;
using UnityEngine;
using UnityEngine.Events;

namespace Exa.UI
{
    public class
        FleetBlueprintViewController : ViewController<FleetBuilderBlueprintView, BlueprintContainer, Blueprint>
    {
        [SerializeField] private GameObject tabPrefab;

        private Dictionary<BlueprintType, BlueprintTypeTabContent> tabsByType =
            new Dictionary<BlueprintType, BlueprintTypeTabContent>();

        private UnityAction<BlueprintContainer> viewButtonCallback;

        private UnityEvent<BlueprintContainer> selectedViewRemoved = new UnityEvent<BlueprintContainer>();

        public void Init(
            UnityAction<BlueprintContainer> viewButtonCallback,
            UnityAction<BlueprintContainer> viewSelectionCallback,
            IObservableEnumerable<BlueprintContainer> enumerable) {
            this.viewButtonCallback = viewButtonCallback;

            selectedViewRemoved.AddListener(viewSelectionCallback);
            Source = enumerable;
        }

        public override void OnAdd(BlueprintContainer value) {
            var tab = tabsByType[value.Data.BlueprintType];

            var view = base.OnAdd(value, tab.container);
            tab.ChildCount++;

            view.ParentTab = tab;
            view.button.onClick.AddListener(() => viewButtonCallback?.Invoke(value));
        }

        public override void OnRemove(BlueprintContainer value) {
            if (GetView(value).Selected)
                selectedViewRemoved.Invoke(value);

            base.OnRemove(value);
        }

        public BlueprintTypeTabContent CreateTab(BlueprintType blueprintType) {
            var tab = Instantiate(tabPrefab, transform)
                .GetComponent<BlueprintTypeTabContent>();
            tab.SetType(blueprintType);
            tabsByType.Add(blueprintType, tab);
            tab.gameObject.SetActive(false);
            return tab;
        }
    }
}