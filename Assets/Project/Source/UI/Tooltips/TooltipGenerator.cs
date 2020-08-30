using Exa.Generics;
using UnityEngine;

namespace Exa.UI.Tooltips
{
    public class TooltipGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject propertyPrefab;
        [SerializeField] private GameObject containerPrefab;
        [SerializeField] private GameObject titlePrefab;
        [SerializeField] private GameObject spacerPrefab;
        [SerializeField] private GameObject textPrefab;

        private Tooltip currentTooltip;

        public Tooltip GenerateTooltip<T>(T value, Transform parent)
            where T : ITooltipPresenter
        {
            var tooltip = value.GetTooltip();
            GenerateTooltipView(tooltip, parent);
            return tooltip;
        }

        public void GenerateTooltipView(Tooltip tooltip, Transform parent)
        {
            currentTooltip = tooltip;
            var container = tooltip.GetContainer();
            container.InstantiateComponentView(parent);
            tooltip.IsDirty = false;
        }

        public PropertyView GenerateTooltipProperty(Transform parent)
        {
            var view = Instantiate(propertyPrefab, parent).GetComponent<PropertyView>();

            if (currentTooltip.Font)
            {
                view.SetFont(currentTooltip.Font);
            }

            return view;
        }

        public ContainerView GenerateTooltipContainer(Transform parent)
        {
            return Instantiate(containerPrefab, parent).GetComponent<ContainerView>();
        }

        public TitleView GenerateTooltipTitle(Transform parent)
        {
            var view = Instantiate(titlePrefab, parent).GetComponent<TitleView>();

            if (currentTooltip.Font)
            {
                view.SetFont(currentTooltip.Font);
            }

            return view;
        }

        public SpacerView GenerateTooltipSpacer(Transform parent)
        {
            return Instantiate(spacerPrefab, parent).GetComponent<SpacerView>();
        }

        public TextView GenerateTooltipText(Transform parent)
        {
            var view = Instantiate(textPrefab, parent).GetComponent<TextView>();

            if (currentTooltip.Font)
            {
                view.SetFont(currentTooltip.Font);
            }

            return view;
        }
    }
}