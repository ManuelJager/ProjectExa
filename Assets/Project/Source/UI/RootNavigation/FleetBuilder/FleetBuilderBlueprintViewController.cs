using System.Collections.Generic;
using System.Linq;
using Exa.Bindings;
using Exa.Grids.Blueprints;
using UnityEngine;

namespace Exa.UI
{
    public class FleetBuilderBlueprintViewController : ViewController<FleetBuilderBlueprintView, BlueprintContainer, Blueprint>
    {
        [SerializeField] private GameObject tabPrefab;
        private Dictionary<BlueprintType, BlueprintTypeTabContent> tabsByType = new Dictionary<BlueprintType, BlueprintTypeTabContent>();

        public override void OnAdd(BlueprintContainer observer)
        {
            var tab = tabsByType[observer.Data.BlueprintType];
            base.OnAdd(observer, tab.container);
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