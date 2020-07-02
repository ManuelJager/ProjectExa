using Exa.Generics;
using Exa.UI.Controls;
using System;
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
        [SerializeField] private string id;
        [SerializeField] private string displayId;
        [SerializeField] private string category;
        [SerializeField] private string displayCategory;
        [SerializeField] private Sprite thumbnail;
        [SerializeField] private Vector2Int size;
        [SerializeField] private GameObject prefab;

        public string Id => id;
        public string DisplayId => displayId;
        public string Category => category;
        public string DisplayCategory => displayCategory;
        public Sprite Thumbnail => thumbnail;
        public Vector2Int Size => size;
        public GameObject Prefab => prefab;

        public abstract void SetValues(IBlock block);

        public virtual ITooltipComponent[] GetComponents()
        {
            return new ITooltipComponent[]
            {
                new NamedValue<string> { Name = "", Value = displayId },
                new NamedValue<string> { Name = "Size", Value = $"{size.x}x{size.y}"}
            };
        }
    }
}