using Exa.Debugging;
using UnityEngine;

namespace Exa.UI
{
    public class MainMenuDebug : DebugBehaviour
    {
        [SerializeField] private GameObject gameEditorButton;

        public override void OnDebugStateChange(bool state)
        {
            gameEditorButton.SetActive(state);
        }
    }
}