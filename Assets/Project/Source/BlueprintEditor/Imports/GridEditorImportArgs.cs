using System;
using System.Collections.Generic;
using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.Validation;

namespace Exa.ShipEditor
{
    public abstract class GridEditorImportArgs
    {
        // Signature for a method that adds a custom validators to the the grid editor
        public delegate (IValidator, Action) AddCustomValidator(Action<ValidationResult> addErrors);
        
        public BlockContext BlockContext { get; set; } = BlockContext.UserGroup;
        public Action OnExit { get; set; }
        public IEnumerable<AddCustomValidator> CustomValidators { get; set; }
        public bool ValidateName { get; protected set; }

        public abstract void Save(Blueprint blueprint);
        public abstract Blueprint GetBlueprint();
    }
}