using Exa.Generics;
using Exa.Grids;
using Exa.UI.Tooltips;
using UnityEngine;

namespace Exa.ShipEditor
{
    public class GridTotalsView : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PropertyView massView;
        [SerializeField] private PropertyView hullView;
        [SerializeField] private PropertyView peakPowerGenerationView;
        [SerializeField] private PropertyView turningPower;

        public void Update()
        {
            var totals = Systems.ShipEditor.editorGrid.blueprintLayer.ActiveBlueprint.Blocks.Totals;
            Render(totals);
        }

        private void Render(GridTotals totals)
        {
            massView.Refresh(new LabeledValue<string>("Mass", $"{totals.Mass:0} KG"));
            hullView.Refresh(new LabeledValue<string>("Hull", $"{totals.Hull:0}"));
            peakPowerGenerationView.Refresh(new LabeledValue<string>("Energy", $"{totals.PowerGenerationModifier:0}"));
            turningPower.Refresh(new LabeledValue<string>("Torque", $"{totals.TurningPowerModifier:0}"));
        }
    }
}