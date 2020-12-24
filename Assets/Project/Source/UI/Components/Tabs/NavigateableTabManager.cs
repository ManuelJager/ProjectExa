using UnityEngine;

#pragma warning disable CS0649

namespace Exa.UI.Components
{
    // TODO: Hide tooltip when not hovering over a tab button
    public class NavigateableTabManager : MonoBehaviour
    {
        [SerializeField] private Navigateable defaultTab;
        private Navigateable activeTab;

        public Navigateable ActiveTab => activeTab;

        private void Start() {
            if (defaultTab != null)
                SetDefaultActive(defaultTab);
        }

        public void SetDefaultActive(Navigateable tab) {
            activeTab = tab;
            activeTab.HandleEnter(null);
        }

        public void SwitchTo(Navigateable newTab) {
            if (newTab == activeTab) return;

            activeTab.NavigateTo(newTab, new NavigationArgs {
                current = activeTab
            });

            activeTab = newTab;
        }
    }
}