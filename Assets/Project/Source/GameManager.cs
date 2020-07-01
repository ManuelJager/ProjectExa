using Exa.Audio;
using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.Grids.Blueprints.Editor;
using Exa.UI;
using Exa.Utils;
using UnityEngine;

namespace Exa
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public static bool IsQuitting { get; private set; }

        public BlockFactory blockFactory;
        public BlueprintManager blueprintManager;
        public ShipEditor shipEditor;
        public PromptController promptController;
        public DiagnosticsPanel diagnosticsPanel;
        public AudioController audioController;

        public void Start()
        {
            shipEditor.editorOverlay.inventory.Source = blockFactory.availibleBlockTemplates;
            // Load blocks from unity scriptable objects
            blockFactory.gameObject.SetActive(true);
            // Load blueprints from disk
            blueprintManager.gameObject.SetActive(true);
        }

        [RuntimeInitializeOnLoadMethod]
        private static void RunOnStart()
        {
            Application.quitting += () =>
            {
                IsQuitting = true;
            };
        }
    }
}