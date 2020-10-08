using Exa.Data;
using Exa.SceneManagement;
using Exa.UI.Components;
using UnityEditor;
using UnityEngine;

namespace Exa.UI
{
    public class RootNavigation : MonoBehaviour
    {
        public BlueprintSelector blueprintSelector;
        public SettingsManager settings;
        public FleetBuilder fleetBuilder;

        [SerializeField] private NavigateableTabManager tabManager;
        [SerializeField] private RootNavigationContent content;

        // TODO: Implement this in an extension of the play button
        public void NavigateToMission()
        {
            var transition = Systems.Scenes.Transition("Mission", new TransitionArgs
            {
                SetActiveScene = true
            });

            transition.onPrepared.AddListener(() =>
            {
                tabManager.ActiveTab.NavigateTo(GameSystems.Navigateable);
            });
        }
    }
}