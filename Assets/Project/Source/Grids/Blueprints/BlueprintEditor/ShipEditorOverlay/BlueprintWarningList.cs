using Exa.Schemas;
using Exa.UI.Controls;
using System;
using UnityEngine.Events;

namespace Exa.Grids.Blueprints
{
    [Serializable]
    public class ValidChangeEvent : UnityEvent<bool>
    {
    }

    public class BlueprintWarningList : ErrorListControl<BlueprintNameValidator, BlueprintValidationArgs>
    {
        public ValidChangeEvent onValidChange;

        public override ValidationSchemaBuilder CreateSchemaBuilder()
        {
            return base.CreateSchemaBuilder()
                .OnValidChange((valid) =>
                {
                    onValidChange.Invoke(valid);
                });
        }
    }
}