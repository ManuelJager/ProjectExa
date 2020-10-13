using System;
using System.Collections.Generic;

namespace Exa.Utils
{
    public static class DictionaryExtensions
    {
        public static void EnsureCreated<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key,
            Func<TValue> factory)
        {
            if (!dictionary.ContainsKey(key))
                dictionary.Add(key, factory());
        }
    }
}