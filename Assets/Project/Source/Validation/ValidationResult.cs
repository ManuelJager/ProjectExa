using Assets.Project.Source.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Exa.Validation
{
    public class ValidationResult : IEnumerable<ValidationError>
    {
        private List<ValidationError> collection;

        public ValidationError this[int index]
        {
            get => collection[index];
            set => collection[index] = value;
        }

        internal ValidationResult()
        {
            collection = new List<ValidationError>();
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
                var error = Activator.CreateInstance(typeof(TError), errorMessage) as TError;
                collection.Add(error);
            }
        }

        /// <summary>
        /// Test the predicate and on false add an error with the given error message
        /// <para>
        /// Will only test when the thrown parameter is false
        /// </para>
        /// </summary>
        /// <typeparam name="TError">Type of error thrown</typeparam>
        /// <param name="errorMessage">Error message</param>
        /// <param name="predicate">Validation condition</param>
        /// <param name="thrown">flag used as reference to keep track wether the assertion should be ran</param>
        public void Assert<TError>(string errorMessage, Func<bool> predicate, ref bool thrown)
            where TError : ValidationError
        {
            if (thrown) return;

            if (!predicate())
            {
                thrown = true;
                var error = Activator.CreateInstance(typeof(TError), errorMessage) as TError;
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
                var error = Activator.CreateInstance(typeof(TError), operationResult.Message) as TError;
                collection.Add(error);
            }
        }

        /// <summary>
        /// Test the predicate and on false add an error with the given error message
        /// <para>
        /// Will only test when the thrown parameter is false
        /// </para>
        /// </summary>
        /// <typeparam name="TError">Type of error thrown</typeparam>
        /// <param name="predicate">Validation contidion</param>
        /// <param name="thrown">flag used as reference to keep track wether the assertion should be ran</param>
        public void Assert<TError>(Func<ValidationOperationResult> predicate, ref bool thrown)
            where TError : ValidationError
        {
            if (thrown) return;

            var operationResult = predicate();
            if (!operationResult.Valid)
            {
                thrown = true;
                var error = Activator.CreateInstance(typeof(TError), operationResult.Message) as TError;
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