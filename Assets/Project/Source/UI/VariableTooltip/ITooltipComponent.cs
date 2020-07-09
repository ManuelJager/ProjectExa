using UnityEngine;

namespace Exa.UI.Tooltips
{
    public struct TooltipComponentBundle
    {
        public GameObject componentView;
        public TooltipComponentBinder componentBinder;
    }

    public interface ITooltipComponent
    {
        TooltipComponentBundle InstantiateComponentView(Transform parent);
    }
}