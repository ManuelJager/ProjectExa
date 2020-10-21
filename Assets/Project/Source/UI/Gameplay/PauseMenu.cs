using System;
using System.Collections;
using System.Collections.Generic;
using Exa.UI.Components;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Exa.UI.Gameplay
{
    public class PauseMenu : Navigateable
    {
        public Navigateable navigateable;

        public Action continueAction;

        public void Continue()
        {
            continueAction();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Load()
        {
            throw new NotImplementedException();
        }

        public void QuitToMenu()
        {
            GameSystems.Navigateable.NavigateTo(Systems.UI.root.navigateable);
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

