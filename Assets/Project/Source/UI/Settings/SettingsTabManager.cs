using Exa.UI.Settings;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Components
{
    public class SettingsTabManager : MonoBehaviour
    {
        [HideInInspector] public SettingsTabBase activeTab;

        [SerializeField] private CanvasGroupInteractibleAdapter applyButton;
        [SerializeField] private CanvasGroupInteractibleAdapter setDefaultButton;
        [SerializeField] private Text activeTabText;
        [SerializeField] private SettingsTabBase defaultTab;
        [SerializeField] private CanvasGroupInteractibleAdapter canvasGroupInteractibleAdapter;

        private void OnEnable()
        {
            ProcessTab(defaultTab);
        }

        /// <summary>
        /// Switches to the given if the values are not dirty
        /// </summary>
        /// <param name="tab"></param>
        public void SetActive(SettingsTabBase tab)
        {
            if (activeTab == tab) return;

            if (activeTab != null && !activeTab.IsDirty)
            {
                ProcessTab(tab);
            }
            else
            {
                QueryUserConfirmation((yes) =>
                {
                    if (yes)
                    {
                        activeTab.ApplyChanges();
                    }

                    ProcessTab(tab);
                });
            }
        }

        public void Update()
        {
            applyButton.Interactable = activeTab.IsDirty;
            setDefaultButton.Interactable = activeTab.IsDirty || !activeTab.IsDefault;
        }

        public void QueryUserConfirmation(Action<bool> onClosePrompt)
        {
            MainManager.Instance.promptController.PromptYesNo(
                "Changes were not saved, do you wish to apply the changes?",
                canvasGroupInteractibleAdapter,
                onClosePrompt);
        }

        /// <summary>
        /// Switches tab
        /// </summary>
        /// <param name="tab"></param>
        public void ProcessTab(SettingsTabBase tab)
        {
            activeTab?.gameObject.SetActive(false);
            activeTab = tab;
            activeTabText.text = tab.tabName;
            tab.gameObject.SetActive(true);
        }

        public void SetDefaultValues()
        {
            activeTab.SetDefaultValues();
        }

        public void ApplyChanges()
        {
            activeTab.ApplyChanges();
        }
    }
}