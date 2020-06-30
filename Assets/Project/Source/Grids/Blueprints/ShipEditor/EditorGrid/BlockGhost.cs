using Exa.Utils;
using UnityEngine;

namespace Exa.Grids.Blueprints.Editor
{
    /// <summary>
    /// Represents a single block that is not yet placed
    /// </summary>
    public class BlockGhost : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer ghostImage;
        [SerializeField] private SpriteRenderer filter;
        [SerializeField] private Transform filterTransform;
        [SerializeField] private Color activeColor;
        [SerializeField] private Color inactiveColor;
        [HideInInspector] public BlueprintBlock blueprintBlock;

        private Vector2Int gridPos;

        public Vector2Int GridPos
        {
            get => gridPos;
            set
            {
                gridPos = value;
                ReflectState();
            }
        }

        public int Rotation
        {
            get => blueprintBlock.Rotation;
            set
            {
                blueprintBlock.Rotation = value;
                ReflectState();
            }
        }

        public AnchoredBlueprintBlock AnchoredBlueprintBlock
        {
            get => new AnchoredBlueprintBlock
            {
                gridAnchor = gridPos,
                blueprintBlock = blueprintBlock
            };
        }

        /// <summary>
        /// Update the block the ghost is representing
        /// </summary>
        /// <param name="block"></param>
        public void ImportBlock(BlueprintBlock block)
        {
            ghostImage.sprite = block.RuntimeContext.Thumbnail;
            filterTransform.localScale = block.RuntimeContext.Size.ToVector3();
            ghostImage.flipX = block.flippedX;
            ghostImage.flipY = block.flippedY;
            this.blueprintBlock = block;
        }

        public void SetFilterColor(bool active)
        {
            filter.color = active ? activeColor : inactiveColor;
        }

        public void ReflectState()
        {
            var realPos = ShipEditorUtils.GetRealPositionByAnchor(blueprintBlock, gridPos);
            transform.localRotation = blueprintBlock.QuaternionRotation;
            transform.localPosition = realPos;
        }
    }
}