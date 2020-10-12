using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Exa.Generics
{
    public class OverrideList<T> : IEnumerable<ValueOverride<T>>
    {
        protected T defaultValue;
        protected readonly Action<T> onValueChange;
        protected List<ValueOverride<T>> overrides = new List<ValueOverride<T>>();

        public OverrideList(T defaultValue, Action<T> onValueChange)
        {
            this.defaultValue = defaultValue;
            this.onValueChange = onValueChange;
        }

        public virtual void Add(ValueOverride<T> valueOverride)
        {
            overrides.Add(valueOverride);
            onValueChange(valueOverride.Value);
        }

        public virtual void Remove(ValueOverride<T> valueOverride)
        {
            if (!overrides.Remove(valueOverride))
            {
                throw new ArgumentException(
                    "To remove a value override, it must be on top of the queue",
                    nameof(valueOverride));
            }

            onValueChange(SelectValue());
        }

        private T SelectValue()
        {
            return overrides.Count == 0
                ? defaultValue
                : overrides.Last().Value;
        }
        
        public IEnumerator<ValueOverride<T>> GetEnumerator()
        {
            return overrides.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}