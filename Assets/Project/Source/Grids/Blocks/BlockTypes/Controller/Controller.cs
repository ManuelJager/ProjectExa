using Exa.Grids.Blocks.Components;

namespace Exa.Grids.Blocks.BlockTypes
{
    public abstract class Controller : Block, IController
    {
        protected override void OnAdd() {
            if (!GridInstance) return;

            GridInstance.Controller = this;
        }

        protected override void OnRemove() {
            if (!GridInstance) return;

            GridInstance.OnControllerDestroyed();
            GridInstance.Controller = null;
        }
    }
}