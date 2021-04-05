using System;
using Exa.Grids.Blueprints;

namespace Exa.ShipEditor
{
    public class ContainerImportArgs : GridEditorImportArgs
    {
        private Action<BlueprintContainer> save;

        public BlueprintContainer Container { get; }

        public ContainerImportArgs(BlueprintContainer container, Action<BlueprintContainer> save) {
            Container = container;

            this.ValidateName = true;
            this.save = save;
        }
        
        public override void Save(Blueprint blueprint) {
            // Set the value of the observable
            Container.SetData(blueprint);
            // Save the blueprint
            save(Container);
        }

        public override Blueprint GetBlueprint() => Container.Data;
    }
}