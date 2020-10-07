using Exa.Debugging;
using Exa.Utils;
using UnityEngine;

namespace Exa.UI
{
    public class MainMenuDebug : DebugBehaviour
    {
        [SerializeField] private GameObject _gameEditorButton;

        public override void OnDebugStateChange(DebugMode state)
        {
            _gameEditorButton.SetActive(state.Is(DebugMode.Global));
        }
    }
}