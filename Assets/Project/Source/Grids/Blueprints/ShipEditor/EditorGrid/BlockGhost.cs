﻿using Exa.Math;
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

        public AnchoredBlueprintBlock AnchoredBlueprintBlock { get; private set; }

        /// <summary>
        /// Update the block the ghost is representing
        /// </summary>
        /// <param name="block"></param>
        public void ImportBlock(BlueprintBlock block)
        {
            ghostImage.sprite = block.RuntimeContext.thumbnail;
            filterTransform.localScale = block.RuntimeContext.size.ToVector3();
            AnchoredBlueprintBlock = new AnchoredBlueprintBlock(new Vector2Int(), block);
            AnchoredBlueprintBlock.UpdateSpriteRenderer(ghostImage);
            AnchoredBlueprintBlock.UpdateLocals(gameObject);
        }

        public void SetFilterColor(bool active)
        {
            filter.color = active ? activeColor : inactiveColor;
        }

        public void ReflectState()
        {
            AnchoredBlueprintBlock.UpdateSpriteRenderer(ghostImage);
            AnchoredBlueprintBlock.UpdateLocals(gameObject);
        }
    }
}