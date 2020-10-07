using System.Collections.Generic;
using UnityEngine;
using Exa.UI;

namespace Exa.UI.Components
{
    public class NavigateableTabManager : MonoBehaviour
    {
        [SerializeField] private Navigateable _defaultTab;
        private Navigateable _activeTab;

        public Navigateable ActiveTab => _activeTab;

        private void Awake()
        {
            _activeTab = _defaultTab;
            _activeTab.HandleEnter(null);
        }

        public void SwitchTo(Navigateable newTab)
        {
            if (newTab == _activeTab) return;

            _activeTab.NavigateTo(newTab, new NavigationArgs
            {
                current = _activeTab
            });

            _activeTab = newTab;
        }
    }
}