using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using Exa.UI.Tooltips;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blocks
{
    /// <summary>
    /// Provides a generic base class for storing and setting the base values of blocks
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BlockTemplate<T> : BlockTemplate
        where T : Block
    {
        [Header("Template partials")]
        [SerializeField] protected PhysicalTemplatePartial physicalTemplatePartial;

        public override IEnumerable<TemplatePartialBase> GetTemplatePartials()
        {
            return new TemplatePartialBase[]
            {
                physicalTemplatePartial
            };
        }
    }

    public abstract class BlockTemplate : ScriptableObject, ITooltipPresenter, IGridTotalsModifier
    {
        [Header("Settings")]
        public string id;
        public string displayId;
        public BlockCategory category;
        public Sprite thumbnail;
        public Vector2Int size;
        public GameObject inertPrefab;
        public GameObject alivePrefab;

        private Tooltip tooltip;

        private void OnEnable()
        {
            tooltip = new Tooltip(SelectTooltipComponents);

            if (!inertPrefab)
            {
                throw new Exception("inertPrefab must have a prefab reference");
            }
        }

        public void AddGridTotals(GridTotals totals)
        {
            foreach (var partial in GetTemplatePartials())
            {
                partial.AddGridTotals(totals);
            }
        }

        public void RemoveGridTotals(GridTotals totals)
        {
            foreach (var partial in GetTemplatePartials())
            {
                partial.RemoveGridTotals(totals);
            }
        }

        public Tooltip GetTooltip()
        {
            return tooltip;
        }

        public abstract IEnumerable<TemplatePartialBase> GetTemplatePartials();

        private IEnumerable<ITooltipComponent> SelectTooltipComponents()
        {
            var components = new List<ITooltipComponent>
            {
                new TooltipTitle(displayId)
            };

            foreach (var partial in GetTemplatePartials())
            {
                foreach (var component in partial.GetTooltipComponents())
                {
                    components.Add(component);
                }
            }

            return components;
        }
    }
}