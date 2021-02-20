using System;
using Exa.UI.Components;

namespace Exa.UI.Gameplay
{
    public class PauseMenu : Navigateable
    {
        public Action continueAction;

        public override void HandleEnter(NavigationArgs args) {
            GameSystems.Raycaster.IsRaycasting = false;
            base.HandleEnter(args);
        }

        public override void HandleExit(Navigateable target) {
            GameSystems.Raycaster.IsRaycasting = true;
            base.HandleExit(target);
        }

        public void Continue() {
            continueAction();
        }

        public void Save() {
            throw new NotImplementedException();
        }

        public void Load() {
            throw new NotImplementedException();
        }

        public void QuitToMenu() {
            GameSystems.Navigateable.NavigateTo(Systems.UI.Root.navigateable);
            Systems.Scenes.UnloadAsync("Game");
            GameSystems.Instance.UnloadMission();
        }

        public void QuitToDesktop() {
            Systems.Quit();
        }
    }
}