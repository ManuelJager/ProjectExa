using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Exa.Types.Generics
{
    public class OverrideList<T> : IEnumerable<IValueOverride<T>>
    {
        protected T defaultValue;
        protected readonly Action<T> onValueChange;
        protected List<IValueOverride<T>> overrides = new List<IValueOverride<T>>();

        public OverrideList(T defaultValue, Action<T> onValueChange) {
            this.defaultValue = defaultValue;
            this.onValueChange = onValueChange;
        }

        public virtual void Add(IValueOverride<T> valueOverride) {
            overrides.Add(valueOverride);
            onValueChange(SelectValue());
        }

        public virtual void Remove(IValueOverride<T> valueOverride) {
            overrides.Remove(valueOverride);
            onValueChange(SelectValue());
        }

        protected virtual T SelectValue() {
            return overrides.Count == 0
                ? defaultValue
                : overrides.Last().Value;
        }

        public IEnumerator<IValueOverride<T>> GetEnumerator() {
            return overrides.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}