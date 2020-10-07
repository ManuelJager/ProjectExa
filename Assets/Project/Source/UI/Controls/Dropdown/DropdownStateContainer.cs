using Exa.Generics;
using System.Collections;
using System.Collections.Generic;

namespace Exa.UI.Controls
{
    public class DropdownStateContainer<T> : IEnumerable<T>
    {
        private readonly HashSet<T> _values = new HashSet<T>();
        private readonly Dictionary<T, LabeledValue<T>> _contextByValue = new Dictionary<T, LabeledValue<T>>();
        private readonly Dictionary<T, DropdownTab> _tabByValue = new Dictionary<T, DropdownTab>();

        public void Add(LabeledValue<T> namedValue, DropdownTab tab)
        {
            _contextByValue.Add(namedValue.Value, namedValue);
            _tabByValue.Add(namedValue.Value, tab);
            _values.Add(namedValue.Value);
        }

        public bool ContainsValue(T value)
        {
            return _values.Contains(value);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        public string GetName(T value)
        {
            try
            {
                return _contextByValue[value].Label;
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException($"value {value} doesn't exist");
            }
        }

        public DropdownTab GetTab(T value)
        {
            return _tabByValue[value];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _values.GetEnumerator();
        }
    }
}