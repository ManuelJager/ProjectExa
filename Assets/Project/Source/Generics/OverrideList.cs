using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Rendering;

namespace Exa.Generics
{
    public class OverrideList<T>
    {
        private T defaultValue;
        private readonly Action<T> onValueChange;
        private List<ValueOverride<T>> overrides = new List<ValueOverride<T>>();

        public OverrideList(T defaultValue, Action<T> onValueChange)
        {
            this.defaultValue = defaultValue;
            this.onValueChange = onValueChange;
        }

        public void Add(ValueOverride<T> valueOverride)
        {
            overrides.Add(valueOverride);
            onValueChange(valueOverride.Value);
        }

        public void Remove(ValueOverride<T> valueOverride)
        {
            if (!overrides.Remove(valueOverride))
            {
                throw new ArgumentException(
                    "To remove a value override, it must be on top of the queue",
                    "valueOverride");
            }

            onValueChange(SelectValue());
        }

        private T SelectValue()
        {
            return overrides.Count == 0
                ? defaultValue
                : overrides.Last().Value;
        }
    }
}