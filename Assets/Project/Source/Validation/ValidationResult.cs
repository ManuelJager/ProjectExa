using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Exa.Validation
{
    public class ValidationResult : IEnumerable<ValidationError>
    {
        private readonly List<ValidationError> collection;

        public IValidator Validator { get; private set; }

        public ValidationResult(IValidator validator)
        {
            collection = new List<ValidationError>();
            Validator = validator;
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
        public void Assert<TError>(string errorMessage, bool predicate)
            where TError : ValidationError
        {
            if (!predicate)
            {
                Throw<TError>(errorMessage);
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
                Throw<TError>(operationResult.Message);
            }
        }

        public IEnumerator<ValidationError> GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        public static implicit operator bool(ValidationResult errors)
        {
            return !ReferenceEquals(errors, null) && errors.Any();
        }
    }
}