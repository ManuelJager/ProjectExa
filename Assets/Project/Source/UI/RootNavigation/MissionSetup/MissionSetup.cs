using Exa.Gameplay.Missions;
using Exa.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Exa.UI
{
    public class MissionSetup : MonoBehaviour
    {
        public MissionOptions options;

        public void NavigateToMission() {
            var transition = Systems.Scenes.Transition("Game", new TransitionArgs {
                loadSceneMode = LoadSceneMode.Additive,
                loadScreenMode = LoadScreenMode.CloseOnPrepared,
                setActiveScene = true,
                reportProgress = true
            });

            transition.onPrepared.AddListener(() => {
                Systems.UI.Root.navigateable.NavigateTo(GameSystems.Navigateable);
                GameSystems.Instance.LoadMission(options.SelectedMission, new MissionArgs {

                });
            });
        }
    }
}