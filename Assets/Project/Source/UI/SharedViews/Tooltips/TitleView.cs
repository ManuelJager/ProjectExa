using Exa.UI.Tooltips;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.SharedViews
{
    public class TitleView : MonoBehaviour
    {
        [SerializeField] private Text text;

        public void Reflect(TooltipTitle data)
        {
            text.text = data.Text;
        }
    }
}