using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Exa.Validation
{
    public class ValidationResult : IEnumerable<ValidationError>
    {
        private List<ValidationError> collection;
        public string ContextID { get; private set; }

        public ValidationError this[int index]
        {
            get => collection[index];
            set => collection[index] = value;
        }

        internal ValidationResult(Type validationContext)
        {
            ContextID = validationContext.Name;
            collection = new List<ValidationError>();
        }

        public void Throw<TError>(string errorMessage)
            where TError : ValidationError
        {
            var error = Activator.CreateInstance(typeof(TError)) as TError;
            error.Message = errorMessage;
            collection.Add(error);
        }

        /// <summary>
        /// Test the predicate and on false add an error with the given error message
        /// </summary>
        /// <typeparam name="TError">Type of error thrown</typeparam>
        /// <param name="errorMessage">Error message</param>
        /// <param name="predicate">Validation condition</param>
        public void Assert<TError>(string errorMessage, Func<bool> predicate)
            where TError : ValidationError
        {
            if (!predicate())
            {
                var error = Activator.CreateInstance(typeof(TError)) as TError;
                error.Message = errorMessage;
                collection.Add(error);
            }
        }

        /// <summary>
        /// Test the predicate and on false add an error with the given error message
        /// </summary>
        /// <typeparam name="TError">Type of error thrown</typeparam>
        /// <param name="predicate">Validation contidion</param>
        public void Assert<TError>(Func<ValidationOperationResult> predicate)
            where TError : ValidationError
        {
            var operationResult = predicate();
            if (!operationResult.Valid)
            {
                var error = Activator.CreateInstance(typeof(TError)) as TError;
                error.Message = operationResult.Message;
                collection.Add(error);
            }
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