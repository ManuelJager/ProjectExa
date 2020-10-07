using Exa.Data;
using Exa.ShipEditor;
using Exa.UI.Controls;
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
        public RootNavigation nav;
        public VariableTooltipManager tooltips;
        public Console console;
        public UserExceptionLogger logger;
        public MouseCursorController mouseCursor;
        public PromptController promptController;
        public DiagnosticsPanel diagnostics;
        public SettingsManager settingsManager;
        public ControlFactory controlFactory;

        private void Awake()
        {
            console.Initialize();
        }
    }
}