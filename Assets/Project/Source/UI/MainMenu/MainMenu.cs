using Exa.SceneManagement;
using Exa.UI.Components;
using UnityEditor;
using UnityEngine;

namespace Exa.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Navigateable menu;
        [SerializeField] private Navigateable blueprintSelector;
        [SerializeField] private Navigateable settings;
        [SerializeField] private Navigateable fleetBuilder;
        [SerializeField] private BlueprintViewController blueprintViewController;

        public void Exit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void NavigateToMission()
        {
            var transition = Systems.Scenes.Transition("Mission", new TransitionArgs
            {
                SetActiveScene = true
            });

            transition.onPrepared.AddListener(() =>
            {
                menu.NavigateTo(GameSystems.Navigateable);
            });
        }

        public void NavigateToEditor()
        {
            menu.NavigateTo(blueprintSelector);
            blueprintViewController.Source = Systems.Blueprints.observableUserBlueprints;
            blueprintViewController.shipEditor.blueprintCollection = Systems.Blueprints.observableUserBlueprints;
        }

        public void NavigateToSettings()
        {
            menu.NavigateTo(settings);
        }

        public void NavigateToFleetBuilder()
        {
            menu.NavigateTo(fleetBuilder);
        }
    }
}