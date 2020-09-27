using System.Collections.Generic;
using UnityEngine;

namespace Exa.UI.Components
{
    public class NavigateableTabManager : MonoBehaviour
    {
        [SerializeField] private Navigateable defaultTab;
        private Navigateable activeTab;

        public Navigateable ActiveTab => activeTab;

        private void Awake()
        {
            activeTab = defaultTab;
            activeTab.OnNavigate(null);
        }

        public void SwitchTo(Navigateable newTab)
        {
            if (newTab == activeTab) return;

            activeTab.NavigateTo(newTab);
            activeTab = newTab;
        }
    }
}