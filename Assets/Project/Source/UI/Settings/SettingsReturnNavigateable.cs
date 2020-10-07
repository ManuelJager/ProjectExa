using Exa.UI.Components;
using UnityEngine;

namespace Exa.UI.Settings
{
    public class SettingsReturnNavigateable : ReturnNavigateable
    {
        [SerializeField] private SettingsTabManager _settingsTabManager;

        protected override void Return(bool force = false)
        {
            if (_settingsTabManager.activeSettingsTab.IsDirty)
            {
                _settingsTabManager.QueryUserConfirmation((yes) =>
                {
                    if (yes)
                    {
                        _settingsTabManager.activeSettingsTab.ApplyChanges();
                    }
                    else
                    {
                        base.Return(true);
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