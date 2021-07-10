using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components {
    [Serializable]
    public class GenericTemplatePartial<T> : TemplatePartial<T>
        where T : struct, IBlockComponentValues {
        [SerializeField] private T data;
        
        public override T ToBaseComponentValues() {
            return data;
        }
    }
}