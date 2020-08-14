using Exa.Generics;
using Exa.Grids;
using Exa.UI.Controls;
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
            massView.Reflect(new LabeledValue<string>("Mass", $"{totals.Mass:0} KG"));
            hullView.Reflect(new LabeledValue<string>("Hull", $"{totals.Hull:0}"));
            peakPowerGenerationView.Reflect(new LabeledValue<string>("Energy", $"{totals.PeakPowerGeneration:0} KW"));
            turningPower.Reflect(new LabeledValue<string>("Torque", $"{totals.TurningPower:0}"));
        }
    }
}