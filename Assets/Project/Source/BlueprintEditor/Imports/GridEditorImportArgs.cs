using System;
using System.Collections;
using System.Collections.Generic;
using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.Validation;

namespace Exa.ShipEditor
{
    public abstract class GridEditorImportArgs
    {
        private List<IPlugableValidator> validators;
        
        public BlockContext BlockContext { get; } = BlockContext.UserGroup;
        public Action OnExit { get; set; }
        public IEnumerable<IPlugableValidator> Validators => validators;
        public bool ValidateName { get; protected set; }

        public GridEditorImportArgs() {
            validators = new List<IPlugableValidator>();
        }
        
        public abstract void Save(Blueprint blueprint);
        public abstract Blueprint GetBlueprint();

        public void AddValidator(IPlugableValidator validator) {
            validators.Add(validator);
        }
    }
}