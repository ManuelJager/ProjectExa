using Exa.UI.Controls;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.Grids.Blueprints.Editor
{
    public class ShipEditorOverlayInfoPanel : MonoBehaviour
    {
        public Button clearButton;
        public Button saveButton;
        public Button exportButton;
        public InputFieldControl blueprintNameInput;
        public BlueprintErrorListController errorListController;
        public CanvasGroup saveButtonCanvasGroup;
    }
}