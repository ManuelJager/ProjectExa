using Exa.Audio;
using Exa.Debugging;
using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.Input;
using Exa.SceneManagement;
using Exa.UI;
using Exa.Utils;
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

#pragma warning disable 649

namespace Exa
{
    public delegate void DebugChangeDelegate(DebugMode mode);

    public class Systems : MonoSingleton<Systems>
    {
        [Header("References")] 
        [SerializeField] private BlockFactory blockFactory;
        [SerializeField] private BlueprintManager blueprintManager;
        [SerializeField] private ShipEditor.ShipEditor shipEditor;
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private ThumbnailGenerator thumbnailGenerator;
        [SerializeField] private DebugManager debugManager;
        [SerializeField] private InputManager inputManager;
        [SerializeField] private ExaSceneManager sceneManager;
        [SerializeField] private LoggerInterceptor logger;
        [SerializeField] private MainUI mainUI;

        [Header("Settings")] 
        [SerializeField] private bool godModeIsEnabled = false;
        [SerializeField] private bool loadSafe = false;

        public static BlockFactory Blocks => Instance.blockFactory;
        public static BlueprintManager Blueprints => Instance.blueprintManager;
        public static ShipEditor.ShipEditor Editor => Instance.shipEditor;
        public static AudioManager Audio => Instance.audioManager;
        public static ThumbnailGenerator Thumbnails => Instance.thumbnailGenerator;
        public static DebugManager Debug => Instance.debugManager;
        public static InputManager Input => Instance.inputManager;
        public static ExaSceneManager Scenes => Instance.sceneManager;
        public static LoggerInterceptor Logger => Instance.logger;
        public static MainUI UI => Instance.mainUI;

        public static bool GodModeIsEnabled {
            get => Instance.godModeIsEnabled;
            set => Instance.godModeIsEnabled = value;
        }

        public static bool IsQuitting { get; set; } = false;

        public static void Quit() {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void Start() {
            var enumerator = loadSafe
                ? EnumeratorUtils.EnumerateSafe(Load(), OnLoadException)
                : Load();

            StartCoroutine(enumerator);
        }

        [RuntimeInitializeOnLoadMethod]
        private static void RunOnStart() {
            Application.quitting += () => { IsQuitting = true; };
        }

        private IEnumerator Load() {
            // Allow the screen to be shown
            UI.wipScreen?.Init();
            UI.loadingScreen.Init();
            UI.loadingScreen.ShowScreen();
            UI.root.gameObject.SetActive(false);

            yield return 0;

            UI.root.settings.Load();
            var targetFrameRate = UI.root.settings.videoSettings.settings.Values.resolution.refreshRate;

            yield return EnumeratorUtils.ScheduleWithFramerate(blockFactory.Init(new Progress<float>(value => {
                var message = $"Loading blocks ({Mathf.RoundToInt(value * 100)}% complete) ...";
                UI.loadingScreen.UpdateMessage(message);
            })), targetFrameRate);

            yield return EnumeratorUtils.ScheduleWithFramerate(blueprintManager.Init(new Progress<float>(value => {
                var message = $"Loading blueprints ({Mathf.RoundToInt(value * 100)}% complete) ...";
                UI.loadingScreen.UpdateMessage(message);
            })), targetFrameRate);

            yield return null;

            UI.root.blueprintSelector.Source = Blueprints.userBlueprints;
            UI.root.missionSetup.fleetBuilder.Init(Blueprints.useableBlueprints);
            UI.root.gameObject.SetActive(true);
            UI.loadingScreen.HideScreen();
        }

        private void OnLoadException(Exception exception) {
            UI.loadingScreen.HideScreen("Error");
            UnityEngine.Debug.LogWarning(exception);
            UI.logger.Log($"An error has occurred while loading.\n {exception.Message}");
        }
    }
}