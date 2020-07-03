﻿using Exa.UI.Components;
using UnityEditor;
using UnityEngine;

namespace Exa.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Navigateable self;
        [SerializeField] private Navigateable shipEditorBlueprintSelector;
        [SerializeField] private Navigateable settings;
        [SerializeField] private BlueprintViewController blueprintViewController;

        public void Exit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void NavigateToEditor()
        {
            self.NavigateTo(shipEditorBlueprintSelector);
            blueprintViewController.Source = GameManager.Instance.blueprintManager.observableUserBlueprints;
            blueprintViewController.shipEditor.blueprintCollection = GameManager.Instance.blueprintManager.observableUserBlueprints;
        }

        public void NavigateToSettings()
        {
            self.NavigateTo(settings);
        }
    }
}