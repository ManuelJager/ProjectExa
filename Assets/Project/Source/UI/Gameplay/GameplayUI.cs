using Exa.UI.Components;
using UnityEngine;

namespace Exa.UI.Gameplay
{
    public class GameplayUI : MonoBehaviour
    {
        public GameplayLayer gameplayLayer;
        public PauseMenu pauseMenu;

        private bool isPaused = false;

        private void Awake() {
            pauseMenu.continueAction = TogglePause;
        }

        public void TogglePause() {
            Navigateable Select(bool revert = false) => isPaused ^ revert
                ? pauseMenu.navigateable
                : gameplayLayer.navigateable;

            Select().NavigateTo(Select(true));
            isPaused = !isPaused;
        }
    }
}