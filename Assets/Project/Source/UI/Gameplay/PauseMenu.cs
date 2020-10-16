using System.Collections;
using System.Collections.Generic;
using Exa.UI.Components;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Exa.UI.Gameplay
{
    public class PauseMenu : Navigateable
    {
        public Navigateable navigateable;
        [SerializeField] private Navigateable gameplayLayer;

        public void QuitToMenu()
        {
            GameSystems.Navigateable.NavigateTo(Systems.UI.nav.navigateable);
            SceneManager.UnloadSceneAsync("Game");
        }

        public void QuitToDesktop()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}

