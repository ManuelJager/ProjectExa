﻿using Exa.Input;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Exa.UI
{
    public class ExtendedInputField : InputField
    {
        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            InputManager.Instance.InputIsCaptured = true;
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            InputManager.Instance.InputIsCaptured = false;
        }
    }
}