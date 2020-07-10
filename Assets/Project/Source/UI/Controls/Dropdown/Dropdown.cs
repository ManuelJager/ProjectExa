using Coffee.UIEffects;
using Exa.Audio;
using Exa.Generics;
using Exa.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Exa.UI.Controls
{
    [Serializable]
    public class DropdownTabSelected : UnityEvent<object>
    {
    }

    [Serializable]
    public class Dropdown : InputControl<NamedWrapper<object>>
    {
        // Stores the tabs by the value hash code
        public Dictionary<NamedWrapper<object>, DropdownTab> tabByOption = new Dictionary<NamedWrapper<object>, DropdownTab>();

        [SerializeField] private Text selectedName;
        [SerializeField] private Text selectedText;
        [SerializeField] private GlobalAudioPlayerProxy playerProxy;
        [SerializeField] private UIFlip tabArrow;
        [SerializeField] private Button button;
        [SerializeField] private Transform tabContainer;
        [SerializeField] private GameObject tabPrefab;
        private NamedWrapper<object> value;
        private Dictionary<object, NamedWrapper<object>> contextByValue = new Dictionary<object, NamedWrapper<object>>();

        public DropdownTabSelected onDropdownTabValueSelected;

        public override NamedWrapper<object> CleanValue { get; set; }

        public override NamedWrapper<object> Value
        {
            get => value;
            set => SetSelected(value.Value);
        }

        private void Awake()
        {
            button.onClick.AddListener(ToggleContainer);
        }

        public virtual void CreateTabs(string name, IEnumerable<NamedWrapper<object>> options)
        {
            this.selectedName.text = name;

            CreateTabs(options);
        }

        public virtual void CreateTabs(IEnumerable<NamedWrapper<object>> options)
        {
            foreach (var option in options)
            {
                var tab = Instantiate(tabPrefab, tabContainer).GetComponent<DropdownTab>();
                tab.Text = option.Name;
                tab.button.onClick.AddListener(() =>
                {
                    SetSelected(option.Value);
                    ToggleContainer();
                });

                tabByOption[option] = tab;
                contextByValue[option.Value] = option;
            }

            foreach (var tab in tabByOption.Values)
            {
                tab.Selected = false;
            }

            SelectFirstActive();
        }

        public virtual void SelectFirstActive()
        {
            SetSelected(
                tabByOption
                .Where((kvp) => kvp.Value.gameObject.activeSelf)
                .FirstOrDefault()
                .Key
                .Value);
        }

        public virtual void SetSelected(object newOptionValue)
        {
            if (tabByOption.ContainsKey(value))
            {
                tabByOption[value].Selected = false;
            }

            var newOption = contextByValue[newOptionValue];

            value = newOption;
            tabByOption[newOption].Selected = true;
            onDropdownTabValueSelected?.Invoke(newOption.Value);
            selectedText.text = newOption.Name;
        }

        private void ToggleContainer()
        {
            var newState = !tabContainer.gameObject.activeSelf;
            tabArrow.vertical = newState;
            tabContainer.gameObject.SetActive(newState);
            playerProxy.Play(newState ? "UI_SFX_ButtonSelectPositive" : "UI_SFX_ButtonSelectNegative");
        }
    }
}