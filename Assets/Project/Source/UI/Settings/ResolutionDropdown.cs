using Exa.UI.Controls;
using UnityEngine;

namespace Exa.UI.Settings
{
    public class ResolutionDropdown : Dropdown
    {
        public void FilterByRefreshRate(int refreshRate)
        {
            foreach (var tab in tabByOption)
            {
                var tabRefreshRate = ((Resolution)tab.Key.Value).refreshRate;
                var matches = tabRefreshRate == refreshRate;
                tab.Value.gameObject.SetActive(matches);
            }
        }
    }
}