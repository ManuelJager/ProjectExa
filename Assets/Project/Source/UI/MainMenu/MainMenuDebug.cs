﻿using Exa.Debugging;
using UnityEngine;

namespace Exa.UI
{
    public class MainMenuDebug : DebugBehaviour
    {
        [SerializeField] private GameObject gameEditorButton;

        public override void OnDebugStateChange(DebugMode state)
        {
            gameEditorButton.SetActive(state.HasFlag(DebugMode.Global));
        }
    }
}