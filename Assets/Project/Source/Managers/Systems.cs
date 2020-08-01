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
using Exa.Utils;
using UnityEngine;

namespace Exa
{
    public class Systems : MonoSingleton<Systems>
    {
        [Header("References")]
        [SerializeField] private BlockFactory blockFactory;
        [SerializeField] private BlueprintManager blueprintManager;
        [SerializeField] private ShipEditor shipEditor;
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private ThumbnailGenerator thumbnailGenerator;
        [SerializeField] private DebugManager debugManager;
        [SerializeField] private InputManager inputManager;
        [SerializeField] private ExaSceneManager sceneManager;
        [SerializeField] private LoggerInterceptor logger;
        [SerializeField] private MainUI mainUI;

        public static BlockFactory BlockFactory => Instance.blockFactory;
        public static BlueprintManager BlueprintManager => Instance.blueprintManager;
        public static ShipEditor ShipEditor => Instance.shipEditor;
        public static AudioManager AudioManager => Instance.audioManager;
        public static ThumbnailGenerator ThumbnailGenerator => Instance.thumbnailGenerator;
        public static DebugManager DebugManager => Instance.debugManager;
        public static InputManager InputManager => Instance.inputManager;
        public static ExaSceneManager SceneManager => Instance.sceneManager;
        public static LoggerInterceptor Logger => Instance.logger;
        public static MainUI MainUI => Instance.mainUI;

        public static bool IsQuitting { get; set; } = false;

        protected override void Awake()
        {
            base.Awake();

            MainUI.loadingScreen.ShowScreen();
        }

        private void Start()
        {
            // Create blocks
            blockFactory.StartUp();

            // Load blueprints from disk
            blueprintManager.StartUp();

            MainUI.loadingScreen.MarkLoaded();
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