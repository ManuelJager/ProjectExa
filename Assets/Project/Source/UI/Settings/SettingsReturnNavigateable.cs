using Exa.UI.Components;
using UnityEngine;

namespace Exa.UI.Settings
{
    public class SettingsReturnNavigateable : ReturnNavigateable
    {
        [SerializeField] private SettingsTabManager settingsTabManager;

        protected override void Return(bool force = false)
        {
            if (settingsTabManager.activeTab.IsDirty)
            {
                settingsTabManager.QueryUserConfirmation((yes) =>
                {
                    if (yes)
                    {
                        settingsTabManager.activeTab.ApplyChanges();
                    }
                    else
                    {
                        base.Return(force);
                    }
                });
            }
            else
            {
                base.Return(force);
            }
        }
    }
}