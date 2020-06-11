using UnityEngine;

namespace Exa.UI.Controls
{
    public interface ITooltipComponent
    {
        GameObject InstantiateComponentView(Transform parent);
    }
}