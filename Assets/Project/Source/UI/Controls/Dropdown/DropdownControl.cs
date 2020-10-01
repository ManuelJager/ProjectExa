using Coffee.UIEffects;
using Exa.Audio;
using Exa.Generics;
using Exa.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
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
        [SerializeField] private RectTransform selectedTab;
        [SerializeField] private RectTransform tabContainer;
        [SerializeField] private GameObject tabPrefab;
        [SerializeField] private InputAction clickAction;
        private object value;

        public override object CleanValue { get; set; }

        public override object Value
        {
            get => value;
            set => SetSelected(value);
        }

        private bool isFoldedOpen;
        private bool IsFoldedOpen
        {
            get => isFoldedOpen;
            set
            {
                isFoldedOpen = value;

                tabContainer.gameObject.SetActive(value);
                tabArrow.vertical = value;
                playerProxy.Play(value ? "UI_SFX_ButtonSelectPositive" : "UI_SFX_ButtonSelectNegative");
            }
        }


        [SerializeField] private DropdownTabSelected onValueChange = new DropdownTabSelected();

        public override UnityEvent<object> OnValueChange => onValueChange;

        private void Awake()
        {
            button.onClick.AddListener(ToggleContainer);
            clickAction.started += context =>
            {
                if (IsFoldedOpen && GetMouseOutsideControl())
                {
                    IsFoldedOpen = false;
                }
            };
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            clickAction.Enable();
        }

        private void OnDisable()
        {
            clickAction.Disable();
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
            IsFoldedOpen = !IsFoldedOpen;
        }

        private bool GetMouseOutsideControl()
        {
            return !Systems.Input.GetMouseInsideRect(tabContainer, selectedTab);
        }

        [Serializable]
        public class DropdownTabSelected : UnityEvent<object>
        {
        }
    }
}