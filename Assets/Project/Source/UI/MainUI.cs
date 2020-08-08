using Exa.Data;
using Exa.Grids.Blueprints.Editor;
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
        public ShipEditorOverlay shipEditorOverlay;
        public MainMenu mainMenu;
        public VariableTooltipManager variableTooltipManager;
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