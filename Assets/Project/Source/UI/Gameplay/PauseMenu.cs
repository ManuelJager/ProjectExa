using System;
using Exa.SceneManagement;
using Exa.UI.Components;
using UnityEngine.SceneManagement;

namespace Exa.UI.Gameplay
{
    public class PauseMenu : Navigateable
    {
        public Navigateable navigateable;

        public Action continueAction;

        public override void HandleEnter(NavigationArgs args)
        {
            base.HandleEnter(args);
            GameSystems.Raycaster.IsRaycasting = false;
        }

        public override void HandleExit(Navigateable target)
        {
            base.HandleExit(target);
            GameSystems.Raycaster.IsRaycasting = true;
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
            GameSystems.Navigateable.NavigateTo(Systems.UI.root.navigateable);
            Systems.Scenes.UnloadAsync("Game");
        }

        public void QuitToDesktop() {
            Systems.Quit();
        }
    }
}