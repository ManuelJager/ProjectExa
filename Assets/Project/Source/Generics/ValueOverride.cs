﻿using UnityEngine;

namespace Exa.Generics
{
    [System.Serializable]
    public class ValueOverride<T>
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