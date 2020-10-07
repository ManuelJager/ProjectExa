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

        [SerializeField] private Text _selectedText;
        [SerializeField] private GlobalAudioPlayerProxy _playerProxy;
        [SerializeField] private UIFlip _tabArrow;
        [SerializeField] private Button _button;
        [SerializeField] private RectTransform _selectedTab;
        [SerializeField] private RectTransform _tabContainer;
        [SerializeField] private GameObject _tabPrefab;
        [SerializeField] private InputAction _clickAction;
        private object _value;

        public override object CleanValue { get; set; }

        public override object Value
        {
            get => _value;
            set => SetSelected(value);
        }

        private bool _isFoldedOpen;
        private bool IsFoldedOpen
        {
            get => _isFoldedOpen;
            set
            {
                _isFoldedOpen = value;

                _tabContainer.gameObject.SetActive(value);
                _tabArrow.vertical = value;
                _playerProxy.Play(value ? "UI_SFX_ButtonSelectPositive" : "UI_SFX_ButtonSelectNegative");
            }
        }


        [SerializeField] private DropdownTabSelected _onValueChange = new DropdownTabSelected();

        public override UnityEvent<object> OnValueChange => _onValueChange;

        private void Awake()
        {
            _button.onClick.AddListener(ToggleContainer);
            _clickAction.started += context =>
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
            _clickAction.Enable();
        }

        private void OnDisable()
        {
            _clickAction.Disable();
        }

        public virtual void CreateTabs(IEnumerable<LabeledValue<object>> options, Action<object, DropdownTab> onTabCreated = null)
        {
            foreach (var option in options)
            {
                var tab = Instantiate(_tabPrefab, _tabContainer).GetComponent<DropdownTab>();
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
            if (stateContainer.ContainsValue(_value))
            {
                stateContainer.GetTab(_value).Selected = false;
            }

            _value = newValue;

            var name = stateContainer.GetName(newValue);

            stateContainer.GetTab(_value).Selected = true; 
            _onValueChange?.Invoke(_value);
            _selectedText.text = name;
        }

        private void ToggleContainer()
        {
            IsFoldedOpen = !IsFoldedOpen;
        }

        private bool GetMouseOutsideControl()
        {
            return !Systems.Input.GetMouseInsideRect(_tabContainer, _selectedTab);
        }

        [Serializable]
        public class DropdownTabSelected : UnityEvent<object>
        {
        }
    }
}