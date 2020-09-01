using Exa.Generics;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Tooltips
{
    [Serializable]
    public class PropertyView : TooltipComponentView<ILabeledValue<object>>
    {
        [Header("References")]
        [SerializeField] private Text keyText;
        [SerializeField] private Text valueText;

        public void SetFont(Font font)
        {
            keyText.font = font;
            valueText.font = font;
        }

        protected override void Refresh(ILabeledValue<object> labeledValue)
        {
            keyText.text = labeledValue.Label;
            valueText.text = labeledValue.Value.ToString();
        }
    }
}