using Exa.Generics;
using System;
using System.Collections.Generic;

namespace Exa.Validation
{
    public class ValidationErrorContainerBuilder : IBuilder<ValidationErrorContainer>
    {
        private readonly ValidationErrorContainer _container;
        private bool _onUnhandledErrorSet;

        /// <summary>
        /// Get the error handler for a given validator
        /// </summary>
        /// <param name="validator">validator identifier</param>
        /// <returns></returns>
        private Dictionary<string, Action<ValidationError>> GetHandlerDictionary(IValidator validator)
        {
            if (!_container.errorHandlers.ContainsKey(validator))
            {
                _container.errorHandlers[validator] = new Dictionary<string, Action<ValidationError>>();
            }
            return _container.errorHandlers[validator];
        }

        /// <summary>
        /// Get the error cleaner for a given validator
        /// </summary>
        /// <param name="validator">validator identifier</param>
        /// <returns></returns>
        private Dictionary<string, Action<ValidationError>> GetCleanerDictionary(IValidator validator)
        {
            if (!_container.errorHandlers.ContainsKey(validator))
            {
                _container.errorHandlers[validator] = new Dictionary<string, Action<ValidationError>>();
            }
            return _container.errorHandlers[validator];
        }

        public ValidationErrorContainerBuilder()
        {
            _container = new ValidationErrorContainer();
            _onUnhandledErrorSet = false;
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
        public ValidationErrorContainerBuilder OnError<TError>(IValidator validator, string id, Action<TError> errorHandler, Action<TError> errorCleaner)
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
            _container.defaultErrorHandler = fallbackHandler;
            _container.defaultErrorCleaner = fallbackCleaner;
            _onUnhandledErrorSet = true;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public ValidationErrorContainer Build()
        {
            if (!_onUnhandledErrorSet)
            {
                throw new BuilderException("OnUnhandledError must be called");
            }

            return _container;
        }
    }
}