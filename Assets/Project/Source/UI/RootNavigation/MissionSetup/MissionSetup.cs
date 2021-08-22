using Exa.Gameplay.Missions;
using Exa.SceneManagement;
using Exa.UI.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Exa.UI {
    public class MissionSetup : AnimatedTabContent {
        public MissionOptions options;

        public void NavigateToMission() {
            var transition = S.Scenes.Transition(
                "Game",
                new TransitionArgs {
                    loadSceneMode = LoadSceneMode.Additive,
                    loadScreenMode = LoadScreenMode.CloseOnPrepared,
                    setActiveScene = true,
                    reportProgress = true
                }
            );

            transition.onPrepared.AddListener(
                () => {
                    S.UI.Root.navigateable.NavigateTo(GS.Navigateable);
                    GS.MissionManager.LoadMission(options.SelectedMission);
                }
            );
        }
    }
}