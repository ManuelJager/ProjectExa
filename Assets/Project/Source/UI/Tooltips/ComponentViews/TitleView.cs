using Exa.UI.Components;
using Exa.UI.Tooltips;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Tooltips
{
    [Serializable]
    public class TitleView : TooltipComponentView<TooltipTitle>
    {
        [SerializeField] private Text _text;

        public void SetFont(Font font)
        {
            _text.font = font;
        }

        public void AddAnimator()
        {
            var animator = _text.gameObject.AddComponent<UiAnimateable>();
            animator.msLocalAnimationOffset = 200f;
        }

        protected override void Refresh(TooltipTitle data)
        {
            _text.text = data.Text;
        }
    }
}