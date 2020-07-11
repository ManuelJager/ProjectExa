using Exa.UI.Components;
using UnityEditor;
using UnityEngine;
using Exa.SceneManagement;

namespace Exa.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Navigateable self;
        [SerializeField] private Navigateable shipEditorBlueprintSelector;
        [SerializeField] private Navigateable settings;
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
            var transition = ExaSceneManager.Instance.Transition("Mission", new TransitionArgs());
            transition.onPrepared.AddListener(() =>
            {
                self.NavigateTo(MissionManager.Instance.navigateable);
            });
        }

        public void NavigateToEditor()
        {
            self.NavigateTo(shipEditorBlueprintSelector);
            blueprintViewController.Source = MainManager.Instance.blueprintManager.observableUserBlueprints;
            blueprintViewController.shipEditor.blueprintCollection = MainManager.Instance.blueprintManager.observableUserBlueprints;
        }

        public void NavigateToSettings()
        {
            self.NavigateTo(settings);
        }
    }
}