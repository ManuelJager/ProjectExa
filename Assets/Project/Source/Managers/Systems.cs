using Exa.Audio;
using Exa.Debugging;
using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.ShipEditor;
using Exa.Input;
using Exa.SceneManagement;
using Exa.UI;
using Exa.Utils;
using System;
using System.Collections;
using Exa.Math;
using UnityEngine;
using UnityEngine.SceneManagement;
#pragma warning disable 649

namespace Exa
{
    public delegate void DebugChangeDelegate(DebugMode mode);

    public class Systems : MonoSingleton<Systems>
    {
        [Header("References")]
        [SerializeField] private BlockFactory _blockFactory;
        [SerializeField] private BlueprintManager _blueprintManager;
        [SerializeField] private ShipEditor.ShipEditor _shipEditor;
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private ThumbnailGenerator _thumbnailGenerator;
        [SerializeField] private DebugManager _debugManager;
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private ExaSceneManager _sceneManager;
        [SerializeField] private LoggerInterceptor _logger;
        [SerializeField] private MainUi _mainUi;

        [Header("Settings")]
        [SerializeField] private bool _godModeIsEnabled = false;

        public static BlockFactory Blocks => Instance._blockFactory;
        public static BlueprintManager Blueprints => Instance._blueprintManager;
        public static ShipEditor.ShipEditor ShipEditor => Instance._shipEditor;
        public static AudioManager Audio => Instance._audioManager;
        public static ThumbnailGenerator Thumbnails => Instance._thumbnailGenerator;
        public static DebugManager Debug => Instance._debugManager;
        public static InputManager Input => Instance._inputManager;
        public static ExaSceneManager Scenes => Instance._sceneManager;
        public static LoggerInterceptor Logger => Instance._logger;
        public static MainUi Ui => Instance._mainUi;

        public static bool GodModeIsEnabled
        {
            get => Instance._godModeIsEnabled;
            set => Instance._godModeIsEnabled = value;
        }

        public static bool IsQuitting { get; set; } = false;

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
            Ui.loadingScreen.ShowScreen();
            yield return 0;

            Ui.settingsManager.Load();

            var targetFrameRate = Ui.settingsManager.videoSettings.current.Values.resolution.refreshRate;

            yield return EnumeratorUtils.ScheduleWithFramerate(_blockFactory.StartUp(new Progress<float>((value) =>
            {
                var message = $"Loading blocks ({Mathf.RoundToInt(value * 100)} % complete) ...";
                Ui.loadingScreen.ShowMessage(message);
            })), targetFrameRate);

            yield return EnumeratorUtils.ScheduleWithFramerate(_blueprintManager.StartUp(new Progress<float>((value) =>
            {
                var message = $"Loading blueprints ({Mathf.RoundToInt(value * 100)} % complete) ...";
                Ui.loadingScreen.ShowMessage(message);
            })), targetFrameRate);

            Ui.nav.blueprintSelector.Source = Blueprints.observableUserBlueprints;
            Ui.nav.blueprintSelector.shipEditor.blueprintCollection = Blueprints.observableUserBlueprints;

            Ui.loadingScreen.HideScreen();
        }
    }
}