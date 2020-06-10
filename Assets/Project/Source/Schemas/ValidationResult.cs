using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Exa.Schemas
{
    public class ValidationResult : IEnumerable<ValidationError>
    {
        private List<ValidationError> collection;
        private int index;
        private string callerName;

        public ValidationError this[int index]
        {
            get => collection[index];
            set => collection[index] = value;
        }

        internal ValidationResult(object caller)
        {
            collection = new List<ValidationError>();
            index = 0;
            callerName = caller.GetType().Name;
        }

        public void Assert<TError>(string errorMessage, Func<bool> predicate)
            where TError : ValidationError
        {
            if (!predicate())
            {
                var error = (TError)Activator.CreateInstance(typeof(TError), errorMessage);
                error.CreatorId = callerName;
                error.Order = index;
                collection.Add(error);
            }
            index++;
        }

        public bool Valid
        {
            get => collection.Count() == 0;
        }

        public IEnumerator<ValidationError> GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return collection.GetEnumerator();
        }
    }
}