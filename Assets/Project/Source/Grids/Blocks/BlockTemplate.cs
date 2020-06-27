using Exa.Generics;
using Exa.UI.Controls;
using System;
using UnityEngine;

namespace Exa.Grids.Blocks
{
    /// <summary>
    /// Provides a generic base class for storing and setting the base values of blocks
    /// </summary>
    /// <typeparam name="TBlock"></typeparam>
    public abstract class BlockTemplate<TBlock> : BlockTemplate
        where TBlock : IBlock
    {
        protected abstract void SetValues(TBlock block);

        public override void SetValues(IBlock block)
        {
            if (block.GetType() == typeof(TBlock))
            {
                SetValues((TBlock)block);
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
                new ValueContext { name = "", value = displayId },
                new ValueContext { name = "Size", value = $"{size.x}x{size.y}"}
            };
        }
    }
}