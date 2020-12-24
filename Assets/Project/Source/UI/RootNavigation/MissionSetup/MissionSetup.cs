using Exa.Gameplay.Missions;
using Exa.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Exa.UI
{
    public class MissionSetup : MonoBehaviour
    {
        public MissionOptions options;
        public FleetBuilder fleetBuilder;

        public void NavigateToMission() {
            var transition = Systems.Scenes.Transition("Game", new TransitionArgs {
                loadSceneMode = LoadSceneMode.Additive,
                loadScreenMode = LoadScreenMode.CloseOnPrepared,
                setActiveScene = true,
                reportProgress = true
            });

            var fleet = fleetBuilder.Fleet;
            transition.onPrepared.AddListener(() => {
                Systems.UI.root.navigateable.NavigateTo(GameSystems.Navigateable);
                GameSystems.Instance.LoadMission(options.SelectedMission, new MissionArgs {
                    fleet = fleet
                });
            });
        }
    }
}