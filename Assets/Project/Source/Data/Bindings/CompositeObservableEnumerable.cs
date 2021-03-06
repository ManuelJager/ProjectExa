﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Exa.Bindings
{
    public class CompositeObservableEnumerable<T> : IObservableEnumerable<T>, ICollectionObserver<T>
    {
        private readonly IObservableEnumerable<T>[] children;

        public List<ICollectionObserver<T>> Observers { get; }

        public IObservableEnumerable<T> Source {
            get => throw new InvalidOperationException("Getting source directly not supported");
            set => throw new InvalidOperationException("Setting source directly not supported");
        }

        public CompositeObservableEnumerable(params IObservableEnumerable<T>[] children) {
            this.children = children;
            Observers = new List<ICollectionObserver<T>>();

            foreach (var child in children)
                child.Observers.Add(this);
        }

        public void OnAdd(T value) {
            foreach (var observer in Observers)
                observer.OnAdd(value);
        }

        public void OnRemove(T value) {
            foreach (var observer in Observers)
                observer.OnRemove(value);
        }

        public IEnumerator<T> GetEnumerator() {
            return children.SelectMany(elem => elem).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}