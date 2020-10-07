using Exa.Generics;
using UnityEngine;

namespace Exa.UI.Tooltips
{
    public class TooltipFactory : MonoBehaviour
    {
        [SerializeField] private GameObject _propertyPrefab;
        [SerializeField] private GameObject _groupPrefab;
        [SerializeField] private GameObject _titlePrefab;
        [SerializeField] private GameObject _spacerPrefab;
        [SerializeField] private GameObject _textPrefab;

        private Tooltip _currentTooltip;

        public GroupView CreateRootView(Tooltip tooltip, Transform parent)
        {
            _currentTooltip = tooltip;
            var container = tooltip.GetRootData();
            var root = container.InstantiateComponentView(parent) as GroupView;
            _currentTooltip = null;
            return root;
        }

        public PropertyView GenerateTooltipProperty(Transform parent, ILabeledValue<object> value)
        {
            var view = CreateComponent<PropertyView>(_propertyPrefab, parent, value);

            if (_currentTooltip.Font)
            {
                view.SetFont(_currentTooltip.Font);
            }

            return view;
        }

        public GroupView GenerateTooltipGroup(Transform parent, TooltipGroup value)
        {
            return CreateComponent<GroupView>(_groupPrefab, parent, value);
        }

        public TitleView GenerateTooltipTitle(Transform parent, TooltipTitle value)
        {
            var view = CreateComponent<TitleView>(_titlePrefab, parent, value);

            if (_currentTooltip.Font)
            {
                view.SetFont(_currentTooltip.Font);
            }

            return view;
        }

        public SpacerView GenerateTooltipSpacer(Transform parent, TooltipSpacer value)
        {
            return CreateComponent<SpacerView>(_spacerPrefab, parent, value);
        }

        public TextView GenerateTooltipText(Transform parent, TooltipText value)
        {
            var view = CreateComponent<TextView>(_textPrefab, parent, value);

            if (_currentTooltip.Font)
            {
                view.SetFont(_currentTooltip.Font);
            }

            return view;
        }

        private T CreateComponent<T>(GameObject prefab, Transform parent, ITooltipComponent value)
            where T : TooltipComponentView
        {
            var component = Instantiate(prefab, parent).GetComponent<T>();
            component.Refresh(value);
            return component;
        }
    }
}