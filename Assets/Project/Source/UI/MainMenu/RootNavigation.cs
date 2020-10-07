using Exa.SceneManagement;
using Exa.UI.Components;
using UnityEditor;
using UnityEngine;

namespace Exa.UI
{
    public class RootNavigation : MonoBehaviour
    {
        [SerializeField] private NavigateableTabManager _tabManager;
        public BlueprintSelector blueprintSelector;

        // TODO: Implement this in an extension of the play button
        public void NavigateToMission()
        {
            var transition = Systems.Scenes.Transition("Mission", new TransitionArgs
            {
                setActiveScene = true
            });

            transition.OnPrepared.AddListener(() =>
            {
                _tabManager.ActiveTab.NavigateTo(GameSystems.Navigateable);
            });
        }
    }
}