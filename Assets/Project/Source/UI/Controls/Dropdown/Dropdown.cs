using Coffee.UIEffects;
using Exa.Audio;
using Exa.Generics;
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

    public class Dropdown : MonoBehaviour
    {
        // Stores the hash code of the currently selected value
        [HideInInspector] public int selectedOption;

        // Stores the tabs by the value hash code
        public Dictionary<int, DropdownTab> tabByOption = new Dictionary<int, DropdownTab>();

        // Stores the tabs by the value option
        public Dictionary<int, object> valueByOption = new Dictionary<int, object>();

        [SerializeField] private Text selectedName;
        [SerializeField] private Text selectedText;
        [SerializeField] private GlobalAudioPlayerProxy playerProxy;
        [SerializeField] private UIFlip tabArrow;
        [SerializeField] private Button button;
        [SerializeField] private Transform tabContainer;
        [SerializeField] private GameObject tabPrefab;

        public DropdownTabSelected onDropdownTabValueSelected;

        private void Awake()
        {
            button.onClick.AddListener(ToggleContainer);
        }

        public void CreateTabs(string selectedName, IEnumerable<NamedValue<object>> options)
        {
            this.selectedName.text = selectedName;

            foreach (var option in options)
            {
                var tab = Instantiate(tabPrefab, tabContainer).GetComponent<DropdownTab>();
                tab.Text = option.Name;
                tab.button.onClick.AddListener(() =>
                {
                    SetSelected(option);
                    ToggleContainer();
                });

                tabByOption[option.Value.GetHashCode()] = tab;
                valueByOption[option.Value.GetHashCode()] = option.Value;
            }

            foreach (var tab in tabByOption.Values)
            {
                tab.Selected = false;
            }

            SetSelected(options.First());
        }

        public void SetSelected(NamedValue<object> option)
        {
            if (tabByOption.ContainsKey(selectedOption))
            {
                tabByOption[selectedOption].Selected = false;
            }

            selectedOption = option.Value.GetHashCode();
            tabByOption[selectedOption].Selected = true;
            onDropdownTabValueSelected?.Invoke(option.Value);
            selectedText.text = option.Name;
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