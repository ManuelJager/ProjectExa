using System;
using Exa.UI.Components;
using UnityEngine.SceneManagement;

namespace Exa.UI.Gameplay
{
    public class PauseMenu : Navigateable
    {
        public Navigateable navigateable;

        public Action continueAction;

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
            SceneManager.UnloadSceneAsync("Game");
        }

        public void QuitToDesktop() {
            Systems.Quit();
        }
    }
}