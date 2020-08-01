using Exa.Audio;
using Exa.Data;
using Exa.Debugging;
using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.Grids.Blueprints.Editor;
using Exa.Grids.Blueprints.Thumbnails;
using Exa.Input;
using Exa.SceneManagement;
using Exa.UI;
using Exa.UI.Tooltips;
using Exa.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace Exa
{
    public class MainManager : MonoSingleton<MainManager>
    {
        [Header("References")]
        public BlockFactory blockFactory;
        public BlueprintManager blueprintManager;
        public ShipEditor shipEditor;
        public AudioManager audioManager;
        public SettingsManager settingsManager;
        public ThumbnailGenerator thumbnailGenerator;
        public DebugManager debugController;
        public InputManager inputManager;
        public ExaSceneManager sceneManager;
        public LoggerInterceptor logger;
        public VariableTooltipManager tooltipManager;
        public PromptController promptController;
        public DiagnosticsPanel diagnostics;
        public UIManager uiManager;

        public static BlockFactory BlockFactory => Instance.blockFactory;
        public static BlueprintManager BlueprintManager => Instance.blueprintManager;
        public static ShipEditor ShipEditor => Instance.shipEditor;
        public static AudioManager AudioManager => Instance.audioManager;
        public static SettingsManager SettingsManager => Instance.settingsManager;
        public static ThumbnailGenerator ThumbnailGenerator => Instance.thumbnailGenerator;

        public static bool IsQuitting { get; set; } = false;

        protected override void Awake()
        {
            base.Awake();

            sceneManager.loadingScreen.ShowScreen();
        }

        private void Start()
        {
            // Create blocks
            blockFactory.StartUp();

            // Load blueprints from disk
            blueprintManager.StartUp();

            sceneManager.loadingScreen.MarkLoaded();
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