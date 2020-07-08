using Exa.UI.Components;
using Exa.UI.Controls;
using Exa.Validation;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    public class BlueprintErrorListController : ErrorListController
    {
        [SerializeField] private LayoutGroupContext layoutGroupContext;

        public override ValidationResult Validate<T>(IValidator<T> validator, T args)
        {
            var result = base.Validate(validator, args);

            layoutGroupContext.UpdateActiveSelf();

            return result;
        }
    }
}