using System.Collections.Generic;
using UnityEngine;
using Exa.UI;

namespace Exa.UI.Components
{
    //TODO: Add selected tab text animation
    public class NavigateableTabManager : MonoBehaviour
    {
        [SerializeField] private Navigateable defaultTab;
        private Navigateable activeTab;

        public Navigateable ActiveTab => activeTab;

        private void Awake()
        {
            activeTab = defaultTab;
            activeTab.HandleEnter(null);
        }

        public void SwitchTo(Navigateable newTab)
        {
            if (newTab == activeTab) return;

            activeTab.NavigateTo(newTab, new NavigationArgs
            {
                current = activeTab
            });

            activeTab = newTab;
        }
    }
}