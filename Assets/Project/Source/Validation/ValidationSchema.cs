using System;
using System.Collections.Generic;
using System.Linq;

namespace Exa.Validation
{
    public class ValidationSchema
    {
        internal Dictionary<string, Action<ValidationError>> errorHandlers = new Dictionary<string, Action<ValidationError>>();
        internal Dictionary<string, Action<ValidationError>> errorCleaners = new Dictionary<string, Action<ValidationError>>();

        internal Action<ValidationError> fallbackHandler;
        internal Action<ValidationError> fallbackCleaner;

        internal IEnumerable<ValidationError> lastControlErrors;
        internal Action<bool> validHandler;
        // Keep track of last validation result
        internal bool? valid;

        public ValidationResult Control<TArgs>(IValidator<TArgs> validator, TArgs args)
        {
            var currectErrors = validator.Validate(args);
            var currentErrorIds = currectErrors.Select(error => error.Id);

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
                var contextId = currentError.Id;
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
                    var lastControlErrorId = lastControlError.Id;

                    if (currentErrorIds.Contains(lastControlErrorId)) continue;

                    if (errorCleaners.ContainsKey(lastControlErrorId))
                    {
                        errorCleaners[lastControlErrorId](lastControlError);
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