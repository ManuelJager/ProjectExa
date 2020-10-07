using Exa.SceneManagement;
using Exa.UI.Components;
using UnityEditor;
using UnityEngine;

namespace Exa.UI
{
    public class RootNavigation : MonoBehaviour
    {
        [SerializeField] private NavigateableTabManager tabManager;
        public BlueprintSelector blueprintSelector;

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