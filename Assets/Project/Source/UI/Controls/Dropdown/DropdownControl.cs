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
    public class DropdownControl : InputControl<object>
    {
        protected DropdownStateContainer<object> stateContainer = new DropdownStateContainer<object>();

        [SerializeField] private Text selectedText;
        [SerializeField] private GlobalAudioPlayerProxy playerProxy;
        [SerializeField] private UIFlip tabArrow;
        [SerializeField] private Button button;
        [SerializeField] private Transform tabContainer;
        [SerializeField] private GameObject tabPrefab;
        private object value;

        public override object CleanValue { get; set; }

        public override object Value
        {
            get => value;
            set => SetSelected(value);
        }

        [SerializeField] private DropdownTabSelected onValueChange = new DropdownTabSelected();

        public override UnityEvent<object> OnValueChange => onValueChange;

        private void Awake()
        {
            button.onClick.AddListener(ToggleContainer);
        }

        public virtual void CreateTabs(IEnumerable<LabeledValue<object>> options, Action<object, DropdownTab> onTabCreated = null)
        {
            foreach (var option in options)
            {
                var tab = Instantiate(tabPrefab, tabContainer).GetComponent<DropdownTab>();
                tab.Text = option.Label;
                tab.button.onClick.AddListener(() =>
                {
                    SetSelected(option.Value);
                    ToggleContainer();
                });

                onTabCreated?.Invoke(option.Value, tab);

                stateContainer.Add(option, tab);
            }

            SelectFirst();
        }

        public virtual void SelectFirst()
        {
            var firstValue = stateContainer.First();

            SetSelected(firstValue);
        }

        public virtual void SetSelected(object newValue)
        {
            if (stateContainer.ContainsValue(value))
            {
                stateContainer.GetTab(value).Selected = false;
            }

            value = newValue;

            var name = stateContainer.GetName(newValue);

            stateContainer.GetTab(value).Selected = true; 
            onValueChange?.Invoke(value);
            selectedText.text = name;
        }

        private void ToggleContainer()
        {
            var newState = !tabContainer.gameObject.activeSelf;
            tabArrow.vertical = newState;
            tabContainer.gameObject.SetActive(newState);
            playerProxy.Play(newState ? "UI_SFX_ButtonSelectPositive" : "UI_SFX_ButtonSelectNegative");
        }

        [Serializable]
        public class DropdownTabSelected : UnityEvent<object>
        {
        }
    }
}