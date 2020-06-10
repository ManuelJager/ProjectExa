using System;
using UnityEngine;

namespace Exa.Grids.Blocks
{
    public abstract class BlockTemplate<TBlock> : BlockTemplate
    {
        public abstract void SetValues(TBlock block);

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

    public abstract class BlockTemplate : ScriptableObject
    {
        public string id;
        public string displayId;
        public string category;
        public string displayCategory;
        public Sprite thumbnail;
        public GameObject prefab;

        private Vector2Int? size = null;

        public Vector2Int Size
        {
            get
            {
                if (size == null)
                {
                    var scale = prefab.transform.localScale;
                    return new Vector2Int
                    {
                        x = Mathf.FloorToInt(scale.x),
                        y = Mathf.FloorToInt(scale.y)
                    };
                }
                return size.GetValueOrDefault();
            }
        }

        public abstract void SetValues(IBlock block);
    }
}