using Exa.Debugging;
using Exa.Utils;
using UnityEngine;

namespace Exa.UI
{
    public class MainMenuDebug : DebugBehaviour
    {
        [SerializeField] private GameObject gameEditorButton;

        public override void OnDebugStateChange(DebugMode state)
        {
            gameEditorButton.SetActive(state.Is(DebugMode.Global));
        }
    }
}