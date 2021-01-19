using UnityEngine;

namespace Exa.Grids
{
    public class BlockPresenter : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        public SpriteRenderer Renderer => spriteRenderer;

        public virtual void Present(IGridMember gridMember) {
            gridMember.UpdateLocals(gameObject);
            gridMember.BlueprintBlock.SetSpriteRendererFlips(spriteRenderer);
        }
    }
}