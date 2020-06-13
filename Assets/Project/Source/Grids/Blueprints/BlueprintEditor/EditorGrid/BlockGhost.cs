using Exa.Utils;
using UnityEngine;

namespace Exa.Grids.Blueprints.BlueprintEditor
{
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

        public void ImportBlock(BlueprintBlock block)
        {
            ghostImage.sprite = block.RuntimeContext.Thumbnail;
            filterTransform.localScale = block.RuntimeContext.Size.ToVector3();
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