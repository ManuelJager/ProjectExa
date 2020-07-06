using UnityEngine;

namespace Exa.UI.Tooltips
{
    public interface ITooltipComponent
    {
        GameObject InstantiateComponentView(Transform parent);
    }
}