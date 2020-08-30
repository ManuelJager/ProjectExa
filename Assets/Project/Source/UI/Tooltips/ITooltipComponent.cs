using UnityEngine;

namespace Exa.UI.Tooltips
{
    public interface ITooltipComponent
    {
        Component InstantiateComponentView(Transform parent);
    }
}