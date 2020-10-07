using System;
using Exa.Grids.Blueprints;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Exa.UI
{
    public class BlueprintTypeSelectEvent : UnityEvent<BlueprintType>
    {
    }

    public class FleetBuilderBlueprintTypes : MonoBehaviour
    {
        public BlueprintTypeSelectEvent selectType = new BlueprintTypeSelectEvent();

        [SerializeField] private Transform _container;
        [SerializeField] private GameObject _buttonPrefab;

        public void BuildList()
        {
            foreach (var blueprintType in Systems.Blueprints.blueprintTypes)
            {
                var blueprintTypeButton = Instantiate(_buttonPrefab, _container).GetComponent<BlueprintTypeButton>();
                blueprintTypeButton.button.onClick.AddListener(() =>
                {
                    selectType?.Invoke(blueprintType);
                });
                blueprintTypeButton.text.text = blueprintType.displayName;
            }
        }
    }
}