using Exa.Grids.Blocks;
using Exa.UI.Tooltips;
using TMPro;
using UnityEngine;

public class BlockCostsView : TooltipComponentView<BlockCosts>
{
    [SerializeField] private TextMeshProUGUI credits;
    [SerializeField] private TextMeshProUGUI metals;
    
    protected override void Refresh(BlockCosts value) {
        credits.text = value.creditCost.ToString();
        metals.text = value.metalsCost.ToString();
    }
}
