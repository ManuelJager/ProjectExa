using Exa.Generics;
using System;

namespace Exa.Validation
{
    public class ValidationSchemaBuilder : IBuilder<ValidationSchema>
    {
        private ValidationSchema schema;
        private bool OnUnhandledErrorSet;

        public ValidationSchemaBuilder()
        {
            schema = new ValidationSchema();
            OnUnhandledErrorSet = false;
        }

        public ValidationSchemaBuilder OnError<TError>(string id, Action<TError> errorHandler, Action<TError> errorCleaner)
            where TError : ValidationError
        {
            schema.errorHandlers[id] = errorHandler as Action<ValidationError>;
            schema.errorCleaners[id] = errorCleaner as Action<ValidationError>;
            return this;
        }

        public ValidationSchemaBuilder OnUnhandledError(Action<ValidationError> fallbackHandler, Action<ValidationError> fallbackCleaner)
        {
            schema.fallbackHandler = fallbackHandler;
            schema.fallbackCleaner = fallbackCleaner;
            OnUnhandledErrorSet = true;
            return this;
        }

        public ValidationSchemaBuilder OnValidChange(Action<bool> validHandler)
        {
            schema.validHandler = validHandler;
            return this;
        }

        public ValidationSchema Build()
        {
            if (!OnUnhandledErrorSet)
            {
                throw new BuilderException("OnUnhandledError must be called");
            }

            return schema;
        }
    }
}