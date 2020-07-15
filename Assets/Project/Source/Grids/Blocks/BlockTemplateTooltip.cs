using Exa.UI.Tooltips;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blocks
{
    [Serializable]
    public class BlockTemplateTooltip : VariableTooltipBase<BlockTemplate>
    {
        [SerializeField] private GameObject tooltipContainerPrefab;
        private Dictionary<string, Transform> tooltips = new Dictionary<string, Transform>();
        private string activeTooltip = "";

        public override void SetValues(BlockTemplate data)
        {
            var result = data.GetComponents();
            var id = data.id;

            // If the given block already has a tooltip
            if (tooltips.ContainsKey(activeTooltip))
            {
                if (activeTooltip == id) return;

                tooltips[activeTooltip].gameObject.SetActive(false);
            }

            // Destroy the tooltip for the given block if the tooltip is out of date
            if (tooltips.ContainsKey(id) && result.IsDirty)
            {
                Destroy(tooltips[id].gameObject);
            }

            // Generate a tooltip if it doesn't already exist or it is out of date
            if (!tooltips.ContainsKey(id) || result.IsDirty)
            {
                var tooltipContainer = Instantiate(tooltipContainerPrefab, transform).transform;
                tooltipContainer.gameObject.name = id;
                var binder = VariableTooltipManager.Instance.tooltipGenerator.GenerateTooltip(data, tooltipContainer);
                binder.Update(data);
                tooltips[id] = tooltipContainer;
            }

            activeTooltip = id;
            container = tooltips[activeTooltip] as RectTransform;
            tooltips[activeTooltip].gameObject.SetActive(true);
        }
    }
}