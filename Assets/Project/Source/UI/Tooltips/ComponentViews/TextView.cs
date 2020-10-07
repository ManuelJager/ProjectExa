using System;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Tooltips
{
    [Serializable]
    public class TextView : TooltipComponentView<TooltipText>
    {
        [SerializeField] private Text _text;

        public void SetFont(Font font)
        {
            _text.font = font;
        }

        protected override void Refresh(TooltipText value)
        {
            _text.text = value.Text;
        }
    }
}