using Exa.Generics;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blueprints;
using Exa.UI.Tooltips;
using Exa.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;

namespace Exa.Grids.Blocks
{
    /// <summary>
    /// Provides a generic base class for storing and setting the base values of blocks
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BlockTemplate<T> : BlockTemplate
        where T : Block
    {
        public virtual void SetValues(T block)
        {
        }

        public override void SetValues(IBlock block)
        {
            if (block.GetType() == typeof(T))
            {
                SetValues((T)block);
            }
            else
            {
                throw new Exception("Incorrect component type given");
            }
        }
    }

    public abstract class BlockTemplate : ScriptableObject, ITooltipPresenter
    {
        public string id;
        public string displayId;
        public string category;
        public string displayCategory;
        public Sprite thumbnail;
        public Vector2Int size;
        public GameObject prefab;

        private Tooltip tooltip;
        private IEnumerable<Func<BlockTemplate, IBlueprintTotalsModifier>> modifierGetters;

        private void OnEnable()
        {
            tooltip = new Tooltip(TooltipComponentFactory);
            modifierGetters = TypeUtils.GetPropertyGetters<BlockTemplate, IBlueprintTotalsModifier>(GetType()).ToList();
        }

        public abstract void SetValues(IBlock block);

        public void DynamicallyAddTotals(Blueprint blueprint)
        {
            foreach (var blueprintModifierGetter in modifierGetters)
            {
                var blueprintModifier = blueprintModifierGetter(this);
                blueprintModifier.AddBlueprintTotals(blueprint);
            }
        }

        public void DynamicallyRemoveTotals(Blueprint blueprint)
        {
            foreach (var blueprintModifierGetter in modifierGetters)
            {
                var blueprintModifier = blueprintModifierGetter(this);
                blueprintModifier.AddBlueprintTotals(blueprint);
            }
        }

        public Tooltip GetTooltip()
        {
            return tooltip;
        }

        protected virtual IEnumerable<ITooltipComponent> TooltipComponentFactory() => new ITooltipComponent[]
        {
            new TooltipTitle(displayId)
        };
    }
}