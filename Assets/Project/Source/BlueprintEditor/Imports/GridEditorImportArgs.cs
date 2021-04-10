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
        public BlockContext BlockContext { get; } = BlockContext.UserGroup;
        public Action OnExit { get; set; }
        public PlugableValidatorBuilder PlugableValidators { get; set; }
        public bool ValidateName { get; protected set; }

        public abstract void Save(Blueprint blueprint);
        public abstract Blueprint GetBlueprint();
    }

    public class PlugableValidatorBuilder : IEnumerable<IPlugableValidator>
    {
        private List<IPlugableValidator> validators = new List<IPlugableValidator>();
        
        public PlugableValidatorBuilder AddValidator(IPlugableValidator validator) {
            validators.Add(validator);
            return this;
        }

        public IEnumerator<IPlugableValidator> GetEnumerator() {
            return validators.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}