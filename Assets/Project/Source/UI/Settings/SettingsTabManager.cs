using Exa.UI.Controls;
using Exa.UI.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Components
{
    public class SettingsTabManager : MonoBehaviour
    {
        [SerializeField] private Text activeTabText;
        [SerializeField] private SettingsTabBase defaultTab;
        [SerializeField] private CanvasGroupInteractibleAdapter canvasGroupInteractibleAdapter;
        private SettingsTabBase activeTab;

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
                GameManager.Instance.promptController.PromptYesNo(
                    "Changes were not saved, do you wish to apply the changes?",
                    canvasGroupInteractibleAdapter,
                    (yes) =>
                    {
                        if (yes)
                        {
                            activeTab.ApplyChanges();
                        }
                        ProcessTab(tab);
                    });
            }
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