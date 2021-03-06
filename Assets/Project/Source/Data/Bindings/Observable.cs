﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Exa.Bindings
{
    /// <summary>
    /// Wrapper that provides a model a way to notify views of changes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Observable<T> : IEquatable<Observable<T>>
        where T : class
    {
        public T Data { get; private set; }

        [JsonIgnore] protected List<IObserver<T>> observers = new List<IObserver<T>>();

        public Observable(T data) {
            this.Data = data;
        }

        public void SetData(T data, bool notify = true) {
            this.Data = data;
            if (notify) Notify();
        }

        public virtual void Notify() {
            foreach (var observer in observers)
                observer.OnUpdate(Data);
        }

        public virtual void Register(IObserver<T> observer) {
            if (observers.Contains(observer)) return;

            observers.Add(observer);
        }

        public virtual void Unregister(IObserver<T> observer) {
            if (!observers.Contains(observer)) return;

            observers.Remove(observer);
        }

        public bool Equals(Observable<T> other) {
            if (other == null) return false;

            return Data.Equals(other.Data);
        }

        public override bool Equals(object obj) {
            if (obj is Observable<T> other)
                return Data.Equals(other.Data);

            return false;
        }

        public static bool operator ==(Observable<T> a, Observable<T> b) {
            if (ReferenceEquals(a, b)) return true;
            if (a is null) return false;
            if (b is null) return false;
            return a.Data.Equals(b.Data);
        }

        public static bool operator !=(Observable<T> a, Observable<T> b) {
            return !(a == b);
        }

        public override int GetHashCode() {
            return Data.GetHashCode();
        }

        public override string ToString() {
            return Data.ToString();
        }
    }
}