using Exa.Generics;
using System;

namespace Exa.Schemas
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

        public ValidationSchemaBuilder OnError<TError>(Action<TError> errorHandler, Action errorCleaner)
            where TError : ValidationError
        {
            var errorType = typeof(TError);
            schema.errorHandlers[errorType] = (Action<object>)errorHandler;
            schema.errorCleaners[errorType] = errorCleaner;
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