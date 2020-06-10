using System;
using UnityEditor;
using UnityEngine;
using Exa.UI.Components;

namespace Exa.UI
{
    [Serializable]
    public enum EditorBlueprintMode
    {
        User,
        Game
    }

    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Navigateable self;
        [SerializeField] private Navigateable shipEditorBlueprintSelector;
        [SerializeField] private BlueprintViewController blueprintViewController;

        public void Exit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void NavigateToEditorProxy(int openModeCode)
        {
            NavigateToEditor((EditorBlueprintMode)openModeCode);
        }

        public void NavigateToEditor(EditorBlueprintMode openMode)
        {
            self.NavigateTo(shipEditorBlueprintSelector);
            switch (openMode)
            {
                case EditorBlueprintMode.User:
                    blueprintViewController.Source = GameManager.Instance.blueprintManager.observableUserBlueprints;
                    blueprintViewController.shipEditor.blueprintCollection = GameManager.Instance.blueprintManager.observableUserBlueprints;
                    break;

                case EditorBlueprintMode.Game:
                    blueprintViewController.Source = GameManager.Instance.blueprintManager.observableGameBlueprints;
                    blueprintViewController.shipEditor.blueprintCollection = GameManager.Instance.blueprintManager.observableGameBlueprints;
                    break;
            }
        }
    }
}