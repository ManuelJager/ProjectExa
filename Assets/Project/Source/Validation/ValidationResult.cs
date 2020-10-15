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

        internal ValidationResult(IValidator validator)
        {
            collection = new List<ValidationError>();
            Validator = validator;
        }

        public TError Throw<TError>(string errorMessage)
            where TError : ValidationError
        {
            var error = Activator.CreateInstance<TError>();
            error.Message = errorMessage;
            collection.Add(error);
            return error;
        }

        public TError Throw<TError>(string errorMessage, ErrorType errorType)
            where TError : ValidationError
        {
            var error = Throw<TError>(errorMessage);
            error.ErrorType = errorType;
            return error;
        }

        public GenericValidationError Throw(string id, string errorMessage)
        {
            var error = new GenericValidationError(id, errorMessage);
            collection.Add(error);
            return error;
        }

        public GenericValidationError Throw(string id, string errorMessage, ErrorType errorType)
        {
            var error = Throw(id, errorMessage);
            error.ErrorType = errorType;
            return error;
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
                Throw<TError>(errorMessage);
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
            return !ReferenceEquals(errors, null) && !errors.Any(error => error.ErrorType == ErrorType.Error);
        }
    }
}