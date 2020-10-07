using Exa.UI.Settings;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Components
{
    public class SettingsTabManager : MonoBehaviour
    {
        [HideInInspector] public SettingsTabBase activeSettingsTab;

        [SerializeField] private CanvasGroupInteractableAdapter _applyButton;
        [SerializeField] private CanvasGroupInteractableAdapter _setDefaultButton;
        [SerializeField] private Text _activeTabText;
        [SerializeField] private SettingsTabBase _defaultSettingsTab;
        [SerializeField] private CanvasGroupInteractableAdapter _canvasGroupInteractableAdapter;

        private void OnEnable()
        {
            ProcessTab(_defaultSettingsTab);
        }

        /// <summary>
        /// Switches to the given if the values are not dirty
        /// </summary>
        /// <param name="settingsTab"></param>
        public void SetActive(SettingsTabBase settingsTab)
        {
            if (activeSettingsTab == settingsTab) return;

            if (activeSettingsTab != null && !activeSettingsTab.IsDirty)
            {
                ProcessTab(settingsTab);
            }
            else
            {
                QueryUserConfirmation((yes) =>
                {
                    if (yes)
                    {
                        activeSettingsTab.ApplyChanges();
                    }

                    ProcessTab(settingsTab);
                });
            }
        }

        public void Update()
        {
            _applyButton.Interactable = activeSettingsTab.IsDirty;
            _setDefaultButton.Interactable = activeSettingsTab.IsDirty || !activeSettingsTab.IsDefault;
        }

        public void QueryUserConfirmation(Action<bool> onClosePrompt)
        {
            Systems.Ui.promptController.PromptYesNo(
                "Changes were not saved, do you wish to apply the changes?",
                _canvasGroupInteractableAdapter,
                onClosePrompt);
        }

        /// <summary>
        /// Switches tab
        /// </summary>
        /// <param name="settingsTab"></param>
        public void ProcessTab(SettingsTabBase settingsTab)
        {
            activeSettingsTab?.gameObject.SetActive(false);
            activeSettingsTab = settingsTab;
            _activeTabText.text = settingsTab.tabName;
            settingsTab.gameObject.SetActive(true);
        }

        public void SetDefaultValues()
        {
            activeSettingsTab.SetDefaultValues();
        }

        public void ApplyChanges()
        {
            activeSettingsTab.ApplyChanges();
        }
    }
}