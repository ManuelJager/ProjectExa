using System;
using Exa.UI.Components;

namespace Exa.UI.Gameplay {
    public class GameOverMenu : Navigateable {
        public ScoreView scoreView;

        public void Retry() {
            throw new NotImplementedException();
        }

        public void QuitToMenu() {
            GS.Navigateable.NavigateTo(Systems.UI.Root.navigateable);
            Systems.Scenes.UnloadAsync("Game");
            GS.MissionManager.UnloadMission();
        }

        public void QuitToDesktop() {
            Systems.Quit();
        }
    }
}