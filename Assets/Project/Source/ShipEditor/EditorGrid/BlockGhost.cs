using Exa.Grids;
using Exa.Grids.Blueprints;
using Exa.Math;
using UnityEngine;

namespace Exa.ShipEditor
{
    /// <summary>
    /// Represents a single block that is not yet placed
    /// </summary>
    public class BlockGhost : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _ghostImage;
        [SerializeField] private SpriteRenderer _filter;
        [SerializeField] private Transform _filterTransform;
        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _inactiveColor;

        public AnchoredBlueprintBlock AnchoredBlueprintBlock { get; private set; }

        /// <summary>
        /// Update the block the ghost is representing
        /// </summary>
        /// <param name="block"></param>
        public void ImportBlock(BlueprintBlock block)
        {
            _ghostImage.sprite = block.Template.thumbnail;
            _filterTransform.localScale = block.Template.size.ToVector3();
            AnchoredBlueprintBlock = new AnchoredBlueprintBlock(new Vector2Int(), block);
            AnchoredBlueprintBlock.BlueprintBlock.SetSpriteRendererFlips(_ghostImage);
            AnchoredBlueprintBlock.UpdateLocals(gameObject);
        }

        public void SetFilterColor(bool active)
        {
            _filter.color = active ? _activeColor : _inactiveColor;
        }

        public void ReflectState()
        {
            AnchoredBlueprintBlock.BlueprintBlock.SetSpriteRendererFlips(_ghostImage);
            AnchoredBlueprintBlock.UpdateLocals(gameObject);
        }
    }
}