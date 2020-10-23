using System;
using System.Collections.Generic;

namespace Exa.Utils
{
    public static class DictionaryExtensions
    {
        public static void EnsureCreated<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key,
            Func<TValue> factory) {
            if (!dictionary.ContainsKey(key))
                dictionary.Add(key, factory());
        }

        public static bool NestedContainsKey<TKey1, TKey2, TValue1>(
            this Dictionary<TKey1, Dictionary<TKey2, TValue1>> dictionary,
            TKey1 key1, TKey2 key2) {
            return
                dictionary.ContainsKey(key1) &&
                dictionary[key1].ContainsKey(key2);
        }
    }
}