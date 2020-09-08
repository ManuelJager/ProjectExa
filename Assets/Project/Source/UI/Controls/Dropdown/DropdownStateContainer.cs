using Exa.Generics;
using System.Collections;
using System.Collections.Generic;

namespace Exa.UI.Controls
{
    public class DropdownStateContainer<T> : IEnumerable<T>
    {
        private readonly HashSet<T> values = new HashSet<T>();
        private readonly Dictionary<T, LabeledValue<T>> contextByValue = new Dictionary<T, LabeledValue<T>>();
        private readonly Dictionary<T, DropdownTab> tabByValue = new Dictionary<T, DropdownTab>();

        public void Add(LabeledValue<T> namedValue, DropdownTab tab)
        {
            contextByValue.Add(namedValue.Value, namedValue);
            tabByValue.Add(namedValue.Value, tab);
            values.Add(namedValue.Value);
        }

        public bool ContainsValue(T value)
        {
            return values.Contains(value);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return values.GetEnumerator();
        }

        public string GetName(T value)
        {
            try
            {
                return contextByValue[value].Label;
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException($"value {value} doesn't exist");
            }
        }

        public DropdownTab GetTab(T value)
        {
            return tabByValue[value];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return values.GetEnumerator();
        }
    }
}