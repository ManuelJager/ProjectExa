using Exa.Generics;
using System;
using System.Collections.Generic;

namespace Exa.Validation
{
    public class ValidationErrorContainerBuilder : IBuilder<ValidationErrorContainer>
    {
        private ValidationErrorContainer container;
        private bool OnUnhandledErrorSet;

        /// <summary>
        /// Get the error handler for a given validator
        /// </summary>
        /// <param name="validator">validator identifier</param>
        /// <returns></returns>
        private Dictionary<string, Action<ValidationError>> GetHandlerDictionary(string validator)
        {
            if (!container.errorHandlers.ContainsKey(validator))
            {
                container.errorHandlers[validator] = new Dictionary<string, Action<ValidationError>>();
            }
            return container.errorHandlers[validator];
        }

        /// <summary>
        /// Get the error cleaner for a given validator
        /// </summary>
        /// <param name="validator">validator identifier</param>
        /// <returns></returns>
        private Dictionary<string, Action<ValidationError>> GetCleanerDictionary(string validator)
        {
            if (!container.errorHandlers.ContainsKey(validator))
            {
                container.errorHandlers[validator] = new Dictionary<string, Action<ValidationError>>();
            }
            return container.errorHandlers[validator];
        }

        public ValidationErrorContainerBuilder()
        {
            container = new ValidationErrorContainer();
            OnUnhandledErrorSet = false;
        }

        /// <summary>
        /// Add error handler and cleaner callback
        /// </summary>
        /// <typeparam name="TError">Type of error</typeparam>
        /// <param name="validator">Validator identifier</param>
        /// <param name="id">Error identifier</param>
        /// <param name="errorHandler">Error handler callback</param>
        /// <param name="errorCleaner">Error cleaner callback</param>
        /// <returns>Builder</returns>
        public ValidationErrorContainerBuilder OnError<TError>(string validator, string id, Action<TError> errorHandler, Action<TError> errorCleaner)
            where TError : ValidationError
        {
            GetHandlerDictionary(validator)[id] = errorHandler as Action<ValidationError>;
            GetCleanerDictionary(validator)[id] = errorCleaner as Action<ValidationError>;
            return this;
        }

        /// <summary>
        /// Add error handler and cleaner callback
        /// </summary>
        /// <param name="errorHandler">Default error handler callback</param>
        /// <param name="errorCleaner">Default error cleaner callback</param>
        /// <returns>Builder</returns>
        public ValidationErrorContainerBuilder OnUnhandledError(Action<ValidationError> fallbackHandler, Action<ValidationError> fallbackCleaner)
        {
            container.defaultErrorHandler = fallbackHandler;
            container.defaultErrorCleaner = fallbackCleaner;
            OnUnhandledErrorSet = true;
            return this;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <returns></returns>
        public ValidationErrorContainer Build()
        {
            if (!OnUnhandledErrorSet)
            {
                throw new BuilderException("OnUnhandledError must be called");
            }

            return container;
        }
    }
}