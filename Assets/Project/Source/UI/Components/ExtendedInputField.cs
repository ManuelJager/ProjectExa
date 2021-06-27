using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Exa.UI.Components {
    public class ExtendedInputField : InputField {
        public override void OnSelect(BaseEventData eventData) {
            base.OnSelect(eventData);
            Systems.Input.inputIsCaptured = true;
        }

        public override void OnDeselect(BaseEventData eventData) {
            base.OnDeselect(eventData);
            Systems.Input.inputIsCaptured = false;
        }
    }
}