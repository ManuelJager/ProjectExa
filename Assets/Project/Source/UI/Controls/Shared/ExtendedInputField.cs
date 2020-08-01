using Exa.Input;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Exa.UI
{
    public class ExtendedInputField : InputField
    {
        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            MainManager.Instance.inputManager.inputIsCaptured = true;
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            MainManager.Instance.inputManager.inputIsCaptured = false;
        }
    }
}