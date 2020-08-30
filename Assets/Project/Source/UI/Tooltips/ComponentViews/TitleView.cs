using Exa.UI.Components;
using Exa.UI.Tooltips;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Tooltips
{
    public class TitleView : MonoBehaviour
    {
        [SerializeField] private Text text;

        public void Reflect(TooltipTitle data)
        {
            text.text = data.Text;
        }

        public void SetFont(Font font)
        {
            text.font = font;
        }

        public void AddAnimator()
        {
            var animator = text.gameObject.AddComponent<UIAnimateable>();
            animator.msLocalAnimationOffset = 200f;
        }
    }
}