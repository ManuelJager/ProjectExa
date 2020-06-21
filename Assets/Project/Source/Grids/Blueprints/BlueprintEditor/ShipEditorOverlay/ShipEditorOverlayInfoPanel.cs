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
        public BlueprintNameErrorList blueprintNameErrorList;
        public BlueprintGridErrorList blueprintGridErrorList;
        public CanvasGroup saveButtonCanvasGroup;
    }
}