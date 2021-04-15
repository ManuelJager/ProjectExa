using System;
using Exa.UI.Components;

namespace Exa.UI.Gameplay
{
    public class PauseMenu : ReturnNavigateable
    {
        public bool Paused { get; private set; }
        
        public override void HandleEnter(NavigationArgs args) {
            GameSystems.Raycaster.IsRaycasting = false;
            Paused = true;
            base.HandleEnter(args);
        }

        public override void HandleExit(Navigateable target) {
            GameSystems.Raycaster.IsRaycasting = true;
            Paused = false;
            base.HandleExit(target);
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
            GameSystems.MissionManager.UnloadMission();
        }

        public void QuitToDesktop() {
            Systems.Quit();
        }
    }
}