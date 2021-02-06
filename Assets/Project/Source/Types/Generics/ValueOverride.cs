using UnityEngine;

namespace Exa.Types.Generics
{
    [System.Serializable]
    public class ValueOverride<T> : IValueOverride<T>
    {
        [SerializeField] private T value;

        public T Value {
            get => value;
            set => this.value = value;
        }

        public ValueOverride(T value) {
            this.value = value;
        }
    }
}