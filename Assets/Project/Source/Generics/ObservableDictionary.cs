using Exa.Bindings;
using System.Collections.Generic;

namespace Exa.Generics
{
    public class ObservableDictionary<TKey, TValue> : ObservableCollection<TValue>
        where TValue : IKeySelector<TKey>
    {
        private readonly Dictionary<TKey, TValue> dict;

        public ObservableDictionary()
        {
            dict = new Dictionary<TKey, TValue>();
        }

        public ObservableDictionary(IEqualityComparer<TKey> comparer)
        {
            dict = new Dictionary<TKey, TValue>(comparer);
        }

        public TValue this[TKey key]
        {
            get => dict[key];
        }

        public override void Add(TValue item)
        {
            dict[KeySelector(item)] = item;
            base.Add(item);
        }

        public override void Clear()
        {
            dict.Clear();
            base.Clear();
        }

        public bool ContainsKey(TKey key)
        {
            return dict.ContainsKey(key);
        }

        public override bool Contains(TValue item)
        {
            return dict.ContainsKey(KeySelector(item));
        }

        public override bool Remove(TValue item)
        {
            var key = KeySelector(item);
            if (dict.ContainsKey(key))
            {
                dict.Remove(key);
            }
            return base.Remove(item);
        }

        protected virtual TKey KeySelector(TValue value)
        {
            return value.Key;
        }
    }
}