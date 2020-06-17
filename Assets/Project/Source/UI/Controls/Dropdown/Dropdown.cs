using Coffee.UIEffects;
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
    public class DropdownTabSelected : UnityEvent<string>
    {
    }

    public class Dropdown : MonoBehaviour
    {
        [HideInInspector] public string selectedOption;
        public DropdownTabSelected onDropdownTabValueSelected;
        public Dictionary<string, DropdownTab> tabByOption = new Dictionary<string, DropdownTab>();

        [SerializeField] private Text selectedName;
        [SerializeField] private Text selectedText;
        [SerializeField] private UIFlip tabArrow;
        [SerializeField] private Button button;
        [SerializeField] private Transform tabContainer;
        [SerializeField] private GameObject tabPrefab;

        private void Awake()
        {
            button.onClick.AddListener(ToggleContainer);
        }

        public void CreateTabs(string selectedName, IEnumerable<ValueContext> options)
        {
            this.selectedName.text = selectedName;

            foreach (var option in options)
            {
                var tab = Instantiate(tabPrefab, tabContainer).GetComponent<DropdownTab>();
                tab.Text = option.name;
                tab.button.onClick.AddListener(() =>
                {
                    SetSelected(option);
                    ToggleContainer();
                });
                tabByOption[option.value] = tab;
            }

            foreach (var tab in tabByOption.Values)
            {
                tab.Selected = false;
            }

            SetSelected(options.First());
        }

        public void SetSelected(ValueContext option)
        {
            if (tabByOption.ContainsKey(selectedOption))
            {
                tabByOption[selectedOption].Selected = false;
            }

            selectedOption = option.value;
            tabByOption[selectedOption].Selected = true;
            onDropdownTabValueSelected?.Invoke(option.value);
            selectedText.text = option.name;
        }

        private void ToggleContainer()
        {
            tabArrow.vertical = !tabArrow.vertical;
            tabContainer.gameObject.SetActive(!tabContainer.gameObject.activeSelf);
        }
    }
}