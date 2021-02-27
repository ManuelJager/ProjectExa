using Exa.UI.Components;
using UnityEngine;

namespace Exa.UI.Gameplay
{
    public class GameplayUI : MonoBehaviour
    {
        public GameplayLayer gameplayLayer;
        public PauseMenu pauseMenu;

        public bool IsPaused { get; private set; }

        private void Awake() {
            pauseMenu.continueAction = TogglePause;
        }

        public void TogglePause() {
            Navigateable Select(bool revert = false) => IsPaused ^ revert
                ? pauseMenu
                : gameplayLayer.navigateable;

            Select().NavigateTo(Select(true));
            IsPaused = !IsPaused;
        }
    }
}
