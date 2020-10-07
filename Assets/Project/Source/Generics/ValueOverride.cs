using UnityEngine;

namespace Exa.Generics
{
    [System.Serializable]
    public class ValueOverride<T>
    {
        [SerializeField] private T value;

        public T Value => value;

        public ValueOverride(T value)
        {
            this.value = value;
        }
    }
}