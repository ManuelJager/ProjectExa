using Exa.Audio;
using Exa.Data;
using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.Grids.Blueprints.Editor;
using Exa.Grids.Blueprints.Thumbnails;
using Exa.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace Exa
{
    public class MainManager : MonoSingleton<MainManager>
    {
        public static bool IsQuitting { get; private set; }
        public static UnityEvent Prepared = new UnityEvent();

        public BlockFactory blockFactory;
        public BlueprintManager blueprintManager;
        public ShipEditor shipEditor;
        public AudioManager audioController;
        public SettingsManager settingsManager;
        public ThumbnailGenerator thumbnailGenerator;

        public void Start()
        {
            MonoSingletonUtils.NotifyCreated<MainManager>(this);
            shipEditor.editorOverlay.inventory.Source = blockFactory.availibleBlockTemplates;
            // Load blueprints from disk
            blueprintManager.gameObject.SetActive(true);
            Prepared?.Invoke();
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