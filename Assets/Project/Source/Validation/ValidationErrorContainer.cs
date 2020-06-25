using System;
using System.Collections.Generic;
using System.Linq;

namespace Exa.Validation
{
    /// <summary>
    /// Used as an error container for multiple validators
    /// </summary>
    public class ValidationErrorContainer
    {
        // Keep track of error handlers in a nested dictionary
        // Access the error handler or cleaner by [validator][error]
        internal Dictionary<string, Dictionary<string, Action<ValidationError>>> errorHandlers;
        internal Dictionary<string, Dictionary<string, Action<ValidationError>>> errorCleaners;

        // Default implementation for the error handlers or cleaners
        internal Action<ValidationError> defaultErrorHandler;
        internal Action<ValidationError> defaultErrorCleaner;

        // Keep track of errors thrown by a specific validator
        internal Dictionary<string, IEnumerable<ValidationError>> lastControlErrors;

        public ValidationErrorContainer()
        {
            errorHandlers = new Dictionary<string, Dictionary<string, Action<ValidationError>>>();
            errorCleaners = new Dictionary<string, Dictionary<string, Action<ValidationError>>>();
            lastControlErrors = new Dictionary<string, IEnumerable<ValidationError>>();
        }

        public ValidationResult Control<TArgs>(IValidator<TArgs> validator, TArgs args)
        {
            // Get errors
            var currErrors = validator.Validate(args);
            var currValidator = currErrors.ContextID;
            var currErrorIds = currErrors.Select(error => error.Id);

            // Error handlers
            foreach (var currentError in currErrors)
            {
                // Check if there is a specific error handler for the error, otherwise use the default one
                if (errorHandlers[currValidator].ContainsKey(currentError.Id))
                {
                    errorHandlers[currValidator][currentError.Id](currentError);
                }
                else
                {
                    defaultErrorHandler(currentError);
                }
            }

            if (lastControlErrors.ContainsKey(currValidator))
            {
                // Get errors from the same validator that were handled last control, but are no longer present
                foreach (var lastControlError in lastControlErrors[currValidator])
                {
                    // If error wasn't thrown in the last control, it doesn't need to be cleaned up
                    if (currErrorIds.Contains(lastControlError.Id)) continue;

                    // Check if there is a specific error handler for the error, otherwise use the default one
                    if (errorCleaners.ContainsKey(lastControlError.Id))
                    {
                        errorCleaners[currValidator][lastControlError.Id](lastControlError);
                    }
                    else
                    {
                        defaultErrorCleaner(lastControlError);
                    }
                }
            }

            // Keep track of the thrown errors for the current validator
            lastControlErrors[currValidator] = currErrors;

            return currErrors;
        }
    }
}