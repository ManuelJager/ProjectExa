﻿using Exa.Generics;
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
    public abstract class BlockTemplate<T> : BlockTemplate
        where T : IBlock
    {
        protected abstract void SetValues(T block);

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

        private void OnEnable()
        {
            tooltip = new Tooltip(TooltipComponentFactory);
        }

        public abstract void SetValues(IBlock block);

        protected virtual IEnumerable<ITooltipComponent> TooltipComponentFactory() => new ITooltipComponent[]
        {
            new TooltipTitle(displayId)
        };

        public Tooltip GetTooltip()
        {
            return tooltip;
        }
    }
}