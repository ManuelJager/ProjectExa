using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Exa.Bindings
{
    /// <summary>
    /// Wrapper that provides a model a way to notify observers of model changes so views are refreshed or queued for refreshing
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class Observable<TData> : IEquatable<Observable<TData>>
        where TData : class
    {
        private TData data;

        public TData Data
        {
            get => data;
            set
            {
                data = value;
                Update();
            }
        }

        [JsonIgnore]
        protected List<IObserver<TData>> observers = new List<IObserver<TData>>();

        public Observable(TData data)
        {
            this.data = data;
        }

        public virtual void Update()
        {
            foreach (var observer in observers)
            {
                observer.OnUpdate(data);
            }
        }

        public virtual void Register(IObserver<TData> observer)
        {
            if (observers.Contains(observer)) return;

            observers.Add(observer);
        }

        public virtual void Unregister(IObserver<TData> observer)
        {
            if (!observers.Contains(observer)) return;

            observers.Remove(observer);
        }

        public bool Equals(Observable<TData> other)
        {
            if (other == null) return false;

            return data.Equals(other.data);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Observable<TData>)) return false;

            if (obj == null) return false;

            var other = obj as Observable<TData>;

            return data.Equals(other.data);
        }

        public static bool operator ==(Observable<TData> a, Observable<TData> b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (ReferenceEquals(a, null)) return false;
            if (ReferenceEquals(b, null)) return false;
            return a.data.Equals(b.data);
        }

        public static bool operator !=(Observable<TData> a, Observable<TData> b)
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