using Exa.Grids.Blocks;
using Exa.UI.Tooltips;
using TMPro;
using UnityEngine;

public class MetadataView : TooltipComponentView<BlockMetadata>
{
    [SerializeField] private TextMeshProUGUI credits;
    [SerializeField] private TextMeshProUGUI metals;
    
    protected override void Refresh(BlockMetadata value) {
        credits.text = value.creditCost.ToString();
        metals.text = value.metalsCost.ToString();
    }
}
