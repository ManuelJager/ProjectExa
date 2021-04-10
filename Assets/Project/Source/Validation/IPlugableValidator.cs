using System;

namespace Exa.Validation
{
    public interface IPlugableValidator : IValidator
    {
        public ValidationResult Result { get; }
        void CleanUp();
    }
}