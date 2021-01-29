using Coffee.UIEffects;
using Exa.Audio;
using Exa.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Types.Generics;
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

        public override object Value {
            get => value;
            protected set {
                if (!stateContainer.ContainsValue(value))
                    throw new ArgumentException($"Value {value} not found", nameof(value));

                if (stateContainer.ContainsValue(this.value))
                    stateContainer.GetTab(this.value).Selected = false;

                this.value = value;
                var name = stateContainer.GetName(value);
                stateContainer.GetTab(value).Selected = true;
                selectedText.text = name;
            }
        }

        private bool isFoldedOpen;

        private bool IsFoldedOpen {
            get => isFoldedOpen;
            set {
                isFoldedOpen = value;

                tabContainer.gameObject.SetActive(value);
                tabArrow.vertical = value;
                playerProxy.Play(value ? "UI_SFX_ButtonSelectPositive" : "UI_SFX_ButtonSelectNegative");
            }
        }


        [SerializeField] private DropdownTabSelected onValueChange = new DropdownTabSelected();

        public override UnityEvent<object> OnValueChange => onValueChange;

        protected virtual void Awake() {
            button.onClick.AddListener(ToggleContainer);
            clickAction.started += context => {
                if (IsFoldedOpen && GetMouseOutsideControl())
                    IsFoldedOpen = false;
            };
        }

        protected void OnEnable() {
            clickAction.Enable();
        }

        private void OnDisable() {
            clickAction.Disable();
        }

        public virtual void CreateTabs(IEnumerable<ILabeledValue<object>> options,
            Action<object, DropdownTab> onTabCreated = null) {
            foreach (var option in options) {
                var tab = Instantiate(tabPrefab, tabContainer).GetComponent<DropdownTab>();
                tab.Text = option.Label;
                tab.button.onClick.AddListener(() => {
                    SetValue(option.Value);
                    ToggleContainer();
                });

                onTabCreated?.Invoke(option.Value, tab);

                stateContainer.Add(option, tab);
            }

            SelectFirst();
        }

        public bool ContainsItem(object item) {
            return stateContainer.ContainsValue(item);
        }

        public virtual void SelectFirst() {
            SetValue(stateContainer.First());
        }

        private void ToggleContainer() {
            IsFoldedOpen = !IsFoldedOpen;
        }

        private bool GetMouseOutsideControl() {
            return !Systems.Input.GetMouseInsideRect(tabContainer, selectedTab);
        }

        [Serializable]
        public class DropdownTabSelected : UnityEvent<object>
        { }
    }
}