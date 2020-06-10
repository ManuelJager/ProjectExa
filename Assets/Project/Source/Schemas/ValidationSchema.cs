using System;
using System.Collections.Generic;
using System.Linq;

namespace Exa.Schemas
{
    public class ValidationSchema
    {
        internal Dictionary<Type, Action<object>> errorHandlers = new Dictionary<Type, Action<object>>();
        internal Dictionary<Type, Action> errorCleaners = new Dictionary<Type, Action>();

        internal Action<ValidationError> fallbackHandler;
        internal Action<ValidationError> fallbackCleaner;

        internal IEnumerable<ValidationError> lastControlErrors;
        internal Action<bool> validHandler;
        internal bool? valid;

        public ValidationResult Control<TArgs>(IValidator<TArgs> validator, TArgs args)
        {
            var currectErrors = validator.Validate(args);
            var currentErrorTypes = currectErrors.Select(error => error.GetType());

            if (valid != null && valid.Value != currectErrors.Valid)
            {
                validHandler(currectErrors.Valid);
            }
            if (valid == null)
            {
                validHandler(currectErrors.Valid);
            }
            valid = currectErrors.Valid;

            // Error handlers
            foreach (var currentError in currectErrors)
            {
                var contextId = currentError.GetType();
                if (errorHandlers.ContainsKey(contextId))
                {
                    errorHandlers[contextId](currentError);
                }
                else
                {
                    fallbackHandler(currentError);
                }
            }

            if (lastControlErrors != null)
            {
                // Get errors that were handled last control, but are no longer present
                foreach (var lastControlError in lastControlErrors)
                {
                    var lastControlErrorType = lastControlError.GetType();

                    if (currentErrorTypes.Contains(lastControlErrorType)) continue;

                    if (errorCleaners.ContainsKey(lastControlErrorType))
                    {
                        errorCleaners[lastControlErrorType]();
                    }
                    else
                    {
                        fallbackCleaner(lastControlError);
                    }
                }
            }

            lastControlErrors = currectErrors;

            return currectErrors;
        }
    }
}