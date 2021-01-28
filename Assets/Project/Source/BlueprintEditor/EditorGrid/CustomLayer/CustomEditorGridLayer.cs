using Exa.Grids.Blueprints;
using UnityEngine;

namespace Exa.ShipEditor
{
    public abstract class CustomEditorGridLayer<T> : CustomEditorGridLayer
    {
        public override void TryAddToGrid(ABpBlock block) {
            if (block.Template is T template) {
                OnAdd(block, template);
            }
        }

        public override void TryRemoveFromGrid(ABpBlock block) {
            if (block.Template is T template) {
                OnRemove(block, template);
            }
        }

        protected abstract void OnAdd(ABpBlock block, T template);
        protected abstract void OnRemove(ABpBlock block, T template);
    }

    public abstract class CustomEditorGridLayer : MonoBehaviour, ICustomEditorGridLayer
    {
        public abstract void TryAddToGrid(ABpBlock block);
        public abstract void TryRemoveFromGrid(ABpBlock block);
    }
}