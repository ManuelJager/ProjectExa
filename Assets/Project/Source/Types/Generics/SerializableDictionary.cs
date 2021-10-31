using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Types.Generics {
    public class SerializableDictionary { }

    [Serializable]
    public class SerializableDictionary<TKey, TValue> : SerializableDictionary, ISerializationCallbackReceiver, IDictionary<TKey, TValue> {
        [SerializeField]
        private List<SerializableKeyValuePair> list = new List<SerializableKeyValuePair>();

        [Serializable]
        public struct SerializableKeyValuePair {
            public TKey key;
            public TValue value;

            public SerializableKeyValuePair(TKey key, TValue value) {
                this.key = key;
                this.value = value;
            }

            public void SetValue(TValue value) {
                this.value = value;
            }
        }

        private Dictionary<TKey, uint> KeyPositions {
            get => keyPositions.Value;
        }

        private Lazy<Dictionary<TKey, uint>> keyPositions;

        public SerializableDictionary() {
            keyPositions = new Lazy<Dictionary<TKey, uint>>(MakeKeyPositions);
        }

        private Dictionary<TKey, uint> MakeKeyPositions() {
            var numEntries = list.Count;
            var result = new Dictionary<TKey, uint>(numEntries);

            for (var i = 0; i < numEntries; i++) {
                result[list[i].key] = (uint) i;
            }

            return result;
        }

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize() {
            // After deserialization, the key positions might be changed
            keyPositions = new Lazy<Dictionary<TKey, uint>>(MakeKeyPositions);
        }

    #region IDictionary<TKey, TValue>

        public TValue this[TKey key] {
            get => list[(int) KeyPositions[key]].value;
            set {
                if (KeyPositions.TryGetValue(key, out uint index)) {
                    list[(int) index].SetValue(value);
                } else {
                    KeyPositions[key] = (uint) list.Count;
                    list.Add(new SerializableKeyValuePair(key, value));
                }
            }
        }

        public ICollection<TKey> Keys {
            get => list.Select(tuple => tuple.key).ToArray();
        }

        public ICollection<TValue> Values {
            get => list.Select(tuple => tuple.value).ToArray();
        }

        public void Add(TKey key, TValue value) {
            if (KeyPositions.ContainsKey(key)) {
                throw new ArgumentException("An element with the same key already exists in the dictionary.");
            }

            KeyPositions[key] = (uint) list.Count;
            list.Add(new SerializableKeyValuePair(key, value));
        }

        public bool ContainsKey(TKey key) => KeyPositions.ContainsKey(key);

        public bool Remove(TKey key) {
            if (KeyPositions.TryGetValue(key, out uint index)) {
                var kp = KeyPositions;
                kp.Remove(key);

                var numEntries = list.Count;

                list.RemoveAt((int) index);

                for (uint i = index; i < numEntries; i++) {
                    kp[list[(int) i].key] = i;
                }

                return true;
            }

            return false;
        }

        public bool TryGetValue(TKey key, out TValue value) {
            if (KeyPositions.TryGetValue(key, out uint index)) {
                value = list[(int) index].value;

                return true;
            }

            value = default;

            return false;
        }

    #endregion

    #region ICollection <KeyValuePair<TKey, TValue>>

        public int Count {
            get => list.Count;
        }

        public bool IsReadOnly {
            get => false;
        }

        public void Add(KeyValuePair<TKey, TValue> kvp) => Add(kvp.Key, kvp.Value);

        public void Clear() => list.Clear();

        public bool Contains(KeyValuePair<TKey, TValue> kvp) => KeyPositions.ContainsKey(kvp.Key);

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) {
            var numKeys = list.Count;

            if (array.Length - arrayIndex < numKeys) {
                throw new ArgumentException("arrayIndex");
            }

            for (var i = 0; i < numKeys; i++, arrayIndex++) {
                var entry = list[i];
                array[arrayIndex] = new KeyValuePair<TKey, TValue>(entry.key, entry.value);
            }
        }

        public bool Remove(KeyValuePair<TKey, TValue> kvp) => Remove(kvp.Key);

    #endregion

    #region IEnumerable <KeyValuePair<TKey, TValue>>

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() {
            return list.Select(ToKeyValuePair).GetEnumerator();

            KeyValuePair<TKey, TValue> ToKeyValuePair(SerializableKeyValuePair skvp) {
                return new KeyValuePair<TKey, TValue>(skvp.key, skvp.value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    #endregion
    }
}