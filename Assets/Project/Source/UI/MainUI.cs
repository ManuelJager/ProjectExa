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
        [SerializeField] private Canvas rootCanvas;
        [SerializeField] private RectTransform rootTransform;
        [SerializeField] private LoadingScreen loadingScreen;
        [SerializeField] private ShipEditorOverlay editorOverlay;
        [SerializeField] private RootNavigation root;
        [SerializeField] private VariableTooltipManager tooltips;
        [SerializeField] private Console console;
        [SerializeField] private NotificationLogger logger;
        [SerializeField] private MouseCursorController mouseCursor;
        [SerializeField] private PromptController promptController;
        [SerializeField] private DiagnosticsPanel diagnostics;
        [SerializeField] private ControlFactory controlFactory;
        [SerializeField] private WipScreen wipScreen;

        public Canvas RootCanvas => rootCanvas;
        public RectTransform RootTransform => rootTransform;
        public LoadingScreen LoadingScreen => loadingScreen;
        public ShipEditorOverlay EditorOverlay => editorOverlay;
        public RootNavigation Root => root;
        public VariableTooltipManager Tooltips => tooltips;
        public Console Console => console;
        public NotificationLogger Logger => logger;
        public MouseCursorController MouseCursor => mouseCursor;
        public PromptController Prompts => promptController;
        public DiagnosticsPanel Diagnostics => diagnostics;
        public ControlFactory Controls => controlFactory;
        public WipScreen WipScreen => wipScreen;

        private void Awake() {
            console.Initialize();
        }
    }
}