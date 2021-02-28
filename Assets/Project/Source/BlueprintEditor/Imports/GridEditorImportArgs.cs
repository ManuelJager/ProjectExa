using System;
using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;

namespace Exa.ShipEditor
{
    public abstract class GridEditorImportArgs
    {
        public BlockContext BlockContext { get; set; } = BlockContext.UserGroup;
        public Action OnExit { get; set; }

        public abstract void Save(Blueprint blueprint);
        public abstract Blueprint GetBlueprint();
    }
}