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
using System;
using System.Collections;
using UnityEngine;

namespace Exa
{
    public delegate void DebugChangeDelegate(bool state);

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
        
        [Header("Settings")]
        [SerializeField] private bool debugIsEnabled = false;
        [SerializeField] private bool godModeIsAnabled = false;

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
        public static bool DebugIsEnabled
        {
            get => Instance.debugIsEnabled;
            set
            {
                Instance.debugIsEnabled = false;
                DebugChange.Invoke(value);
            }
        }
        public static bool GodModeIsAnabled
        {
            get => Instance.godModeIsAnabled;
            set => Instance.godModeIsAnabled = value;
        }

        public static bool IsQuitting { get; set; } = false;

        public static event DebugChangeDelegate DebugChange;

        private void Start()
        {
            StartCoroutine(Load());
        }

        [RuntimeInitializeOnLoadMethod]
        private static void RunOnStart()
        {
            Application.quitting += () =>
            {
                IsQuitting = true;
            };
        }

        private IEnumerator Load()
        {
            // Allow the screen to be shown
            MainUI.loadingScreen.ShowScreen();
            yield return 0;

            var targetFrameRate = MainUI.settingsManager.videoSettings.current.Values.resolution.refreshRate;

            yield return EnumeratorUtils.ScheduleWithFramerate(blockFactory.StartUp(new Progress<float>((value) =>
            {
                var message = $"Loading blocks ({Mathf.RoundToInt(value * 100)} % complete) ...";
                MainUI.loadingScreen.ShowMessage(message);
            })), targetFrameRate);

            yield return EnumeratorUtils.ScheduleWithFramerate(blueprintManager.StartUp(new Progress<float>((value) =>
            {
                var message = $"Loading blueprints ({Mathf.RoundToInt(value * 100)} % complete) ...";
                MainUI.loadingScreen.ShowMessage(message);
            })), targetFrameRate); 

            MainUI.loadingScreen.HideScreen();
        }
    }
}