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
        [SerializeField] private Text _keyText;
        [SerializeField] private Text _valueText;

        public void SetFont(Font font)
        {
            _keyText.font = font;
            _valueText.font = font;
        }

        public void SetValue(object value)
        {
            _valueText.text = value.ToString();
        }

        protected override void Refresh(ILabeledValue<object> labeledValue)
        {
            _keyText.text = labeledValue.Label;
            _valueText.text = labeledValue.Value.ToString();
        }
    }
}