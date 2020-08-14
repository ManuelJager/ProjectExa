using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using Exa.UI.Tooltips;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private PhysicalTemplatePartial physicalTemplatePartial;

        public PhysicalTemplatePartial PhysicalTemplatePartial
        {
            get => physicalTemplatePartial;
            set => physicalTemplatePartial = value;
        }

        public override Block AddBlockOnGameObject(GameObject gameObject)
        {
            return BuildOnGameObject(gameObject);
        }

        protected virtual T BuildOnGameObject(GameObject gameObject)
        {
            var instance = gameObject.AddComponent<T>();

            foreach (var partial in GetTemplatePartials())
            {
                partial.AddSelf(instance);
            }

            return instance;
        }

        protected override IEnumerable<TemplatePartialBase> GetTemplatePartials()
        {
            return new TemplatePartialBase[]
            {
                physicalTemplatePartial
            };
        }
    }

    public abstract class BlockTemplate : ScriptableObject, ITooltipPresenter, IGridTotalsModifier
    {
        public string id;
        public string displayId;
        public string category;
        public string displayCategory;
        public Sprite thumbnail;
        public Vector2Int size;
        public GameObject inertPrefab;
        public GameObject alivePrefab;

        private Tooltip tooltip;

        public bool GeneratePrefab { get; private set; }

        private void OnEnable()
        {
            tooltip = new Tooltip(SelectTooltipComponents);
            if (!inertPrefab) throw new Exception("inertPrefab must have a prefab reference");
            GeneratePrefab = !alivePrefab;
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

        public void SetValues(Block block)
        {
            foreach (var partial in GetTemplatePartials())
            {
                partial.SetValues(block);
            }
        }

        public abstract Block AddBlockOnGameObject(GameObject gameObject);

        protected abstract IEnumerable<TemplatePartialBase> GetTemplatePartials();

        private IEnumerable<ITooltipComponent> SelectTooltipComponents()
        {
            var components = new ITooltipComponent[]
            {
                new TooltipTitle(displayId)
            } as IEnumerable<ITooltipComponent>;

            foreach (var partial in GetTemplatePartials())
            {
                components.Concat(partial.GetTooltipComponents());
            }

            return components;
        }
    }
}