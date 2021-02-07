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
        public Canvas rootCanvas;
        public RectTransform rootTransform;
        public LoadingScreen loadingScreen;
        public ShipEditorOverlay editorOverlay;
        public RootNavigation root;
        public VariableTooltipManager tooltips;
        public Console console;
        public NotificationLogger logger;
        public MouseCursorController mouseCursor;
        public PromptController promptController;
        public DiagnosticsPanel diagnostics;
        public ControlFactory controlFactory;
        public WipScreen wipScreen;

        private void Awake() {
            console.Initialize();
        }
    }
}