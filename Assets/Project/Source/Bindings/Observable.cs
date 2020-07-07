using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Exa.Bindings
{
    /// <summary>
    /// Wrapper that provides a model a way to notify observers of model changes so views are refreshed or queued for refreshing
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Observable<T> : IEquatable<Observable<T>>
        where T : class
    {
        private T data;

        public T Data
        {
            get => data;
        }

        [JsonIgnore]
        protected List<IObserver<T>> observers = new List<IObserver<T>>();

        public Observable(T data)
        {
            this.data = data;
        }

        public void SetData(T data, bool notify = true)
        {
            this.data = data;
            if (notify) Notify();
        }

        public virtual void Notify()
        {
            foreach (var observer in observers)
            {
                observer.OnUpdate(data);
            }
        }

        public virtual void Register(IObserver<T> observer)
        {
            if (observers.Contains(observer)) return;

            observers.Add(observer);
        }

        public virtual void Unregister(IObserver<T> observer)
        {
            if (!observers.Contains(observer)) return;

            observers.Remove(observer);
        }

        public bool Equals(Observable<T> other)
        {
            if (other == null) return false;

            return data.Equals(other.data);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Observable<T>)) return false;

            if (obj == null) return false;

            var other = obj as Observable<T>;

            return data.Equals(other.data);
        }

        public static bool operator ==(Observable<T> a, Observable<T> b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (ReferenceEquals(a, null)) return false;
            if (ReferenceEquals(b, null)) return false;
            return a.data.Equals(b.data);
        }

        public static bool operator !=(Observable<T> a, Observable<T> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return data.GetHashCode();
        }

        public override string ToString()
        {
            return data.ToString();
        }
    }
}