using System;
using System.Collections.Generic;
using Exa.Utils;

namespace Exa.Types {
    public class DefaultDict<TKey, TValue> : Dictionary<TKey, TValue> {
        private readonly Func<TKey, TValue> factory;

        public DefaultDict(Func<TKey, TValue> factory) {
            this.factory = factory;
        }

        public new TValue this[TKey key] {
            get {
                if (!ContainsKey(key)) {
                    Add(key, value: factory(key));
                }

                return base[key];
            }
            set => base[key] = value;
        }
    }
}