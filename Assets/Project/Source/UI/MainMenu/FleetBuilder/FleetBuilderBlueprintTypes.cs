using Exa.Grids.Blueprints;
using UnityEngine;
using UnityEngine.Events;
#pragma warning disable CS0649

namespace Exa.UI
{
    public class BlueprintTypeSelectEvent : UnityEvent<BlueprintType>
    {
    }

    public class FleetBuilderBlueprintTypes : MonoBehaviour
    {
        public BlueprintTypeSelectEvent SelectType = new BlueprintTypeSelectEvent();

        [SerializeField] private Transform container;
        [SerializeField] private GameObject buttonPrefab;

        public void BuildList()
        {
            foreach (var blueprintType in Systems.Blueprints.blueprintTypes)
            {
                var blueprintTypeButton = Instantiate(buttonPrefab, container).GetComponent<BlueprintTypeButton>();
                blueprintTypeButton.button.onClick.AddListener(() =>
                {
                    SelectType?.Invoke(blueprintType);
                });
                blueprintTypeButton.text.text = blueprintType.displayName;
            }
        }
    }
}