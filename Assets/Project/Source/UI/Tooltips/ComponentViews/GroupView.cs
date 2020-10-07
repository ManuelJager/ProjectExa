using Exa.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Tooltips
{
    [Serializable]
    public class GroupView : TooltipComponentView<TooltipGroup>
    {
        public Transform container;

        [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup;
        private List<TooltipComponentView> _views;

        protected override void Refresh(TooltipGroup value)
        {
            _verticalLayoutGroup.padding.left = value.Tabs * 8;
            _verticalLayoutGroup.spacing = value.Spacing;

            if (_views == null)
            {
                _views = new List<TooltipComponentView>();
                foreach (var child in value.Children)
                {
                    _views.Add(child.InstantiateComponentView(container));
                }
            }
            else
            {
                foreach (var tuple in EnumerableUtils.AsTupleEnumerable(value.Children, _views))
                {
                    tuple.Item2.Refresh(tuple.Item1);
                }
            }
        }
    }
}