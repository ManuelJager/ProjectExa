using System;

namespace Exa.Generics
{
    /// <summary>
    /// Supports a value with lazy initialization that can be invalidated
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LazyCache<T>
    {
        protected Func<T> valueFactory;
        protected T value;
        protected bool valueUpdated = false;

        public T Value {
            get {
                // If value is not up to date, create it and set the flag
                if (!valueUpdated) {
                    value = valueFactory();
                    valueUpdated = true;
                }

                return value;
            }
        }

        public LazyCache() { }

        public LazyCache(Func<T> valueFactory) {
            this.valueFactory = valueFactory;
        }

        /// <summary>
        /// Invalidate a value so it needs to be recalculated next time it's requested
        /// </summary>
        public void Invalidate() {
            valueUpdated = false;
        }
    }
}