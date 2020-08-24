using UnityEngine;

namespace Exa.UI.Tooltips
{
    public struct TooltipComponentBundle
    {
        public GameObject componentView;
    }

    public interface ITooltipComponent
    {
        TooltipComponentBundle InstantiateComponentView(Transform parent);
    }
}