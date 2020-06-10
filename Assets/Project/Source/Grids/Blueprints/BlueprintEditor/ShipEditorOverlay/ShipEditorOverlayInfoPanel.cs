using Exa.UI;
using Exa.UI.Controls;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.Grids.Blueprints.BlueprintEditor
{
    public class ShipEditorOverlayInfoPanel : MonoBehaviour
    {
        public Button clearButton;
        public Button saveButton;
        public NamedInputField blueprintNameInput;
        public BlueprintWarningList blueprintWarningList;
        public CanvasGroup saveButtonCanvasGroup;
    }
}