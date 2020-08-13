using Exa.Data;
using Exa.ShipEditor;
using Exa.UI.Diagnostics;
using Exa.UI.Tooltips;
using UCommandConsole;
using UnityEngine;

namespace Exa.UI
{
    public class MainUI : MonoBehaviour
    {
        public Canvas root;
        public RectTransform rootTransform;
        public LoadingScreen loadingScreen;
        public ShipEditorOverlay editorOverlay;
        public MainMenu mainMenu;
        public VariableTooltipManager tooltips;
        public Console console;
        public UserExceptionLogger userExceptionLogger;
        public MouseCursorController mouseCursor;
        public PromptController promptController;
        public DiagnosticsPanel diagnostics;
        public SettingsManager settingsManager;

        private void Awake()
        {
            console.Initialize();
        }
    }
}