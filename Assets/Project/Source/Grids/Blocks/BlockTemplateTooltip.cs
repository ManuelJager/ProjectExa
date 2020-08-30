using Exa.UI.Tooltips;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blocks
{
    [Serializable]
    public class BlockTemplateTooltip : TooltipBase
    {
        [SerializeField] private GameObject tooltipContainerPrefab;
        private ContextDict contextDict = new ContextDict();
        private string activeTooltip = "";

        public void SetValues(BlockContext blockContext, BlockTemplate data)
        {
            var result = Systems.Blocks.valuesStore.GetTooltip(blockContext, data.id);
            var id = data.id;
            var tooltipDict = EnsureCreated(blockContext);

            // If the given block already has a tooltip
            if (tooltipDict.ContainsKey(activeTooltip))
            {
                if (activeTooltip == id) return;

                tooltipDict[activeTooltip].gameObject.SetActive(false);
            }

            // Destroy the tooltip for the given block if the tooltip is out of date
            if (tooltipDict.ContainsKey(id) && result.IsDirty)
            {
                Destroy(tooltipDict[id].gameObject);
            }

            // Generate a tooltip if it doesn't already exist or it is out of date
            if (!tooltipDict.ContainsKey(id) || result.IsDirty)
            {
                var tooltipContainer = Instantiate(tooltipContainerPrefab, transform).transform as RectTransform;
                tooltipContainer.gameObject.name = id;
                Systems.UI.tooltips.tooltipGenerator.GenerateTooltipView(result, tooltipContainer);
                tooltipDict[id] = tooltipContainer;
            }

            activeTooltip = id;
            container = tooltipDict[activeTooltip];
            tooltipDict[activeTooltip].gameObject.SetActive(true);
        }

        public void ShowTooltip(BlockContext blockContext, BlockTemplate data)
        {
            gameObject.SetActive(true);
            SetValues(blockContext, data);
            SetContainerPosition();
        }

        public void HideTooltip()
        {
            gameObject.SetActive(false);
        }

        private TooltipDict EnsureCreated(BlockContext blockContext)
        {
            if (!contextDict.ContainsKey(blockContext))
            {
                contextDict.Add(blockContext, new TooltipDict());
            }

            return contextDict[blockContext];
        }

        private class ContextDict : Dictionary<BlockContext, TooltipDict>
        {
        }

        private class TooltipDict : Dictionary<string, RectTransform>
        {
        }
    }
}