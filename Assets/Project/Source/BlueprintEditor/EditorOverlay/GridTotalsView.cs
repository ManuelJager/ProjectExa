using Exa.Grids;
using Exa.UI.Tooltips;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.ShipEditor
{
    public class GridTotalsView : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PropertyView massView;
        [SerializeField] private PropertyView hullView;
        [SerializeField] private PropertyView energyView;
        [SerializeField] private PropertyView turningPower;

        public void Update() {
            var totals = Systems.Editor.editorGrid.blueprintLayer.ActiveBlueprint?.Blocks.GetTotals();
            if (totals != null) {
                Render(totals);
            }
        }

        private void Render(GridTotals totals) {
            massView.SetValue($"{totals.Mass:0} Tonne");
            hullView.SetValue($"{totals.Hull:0}");
            energyView.SetValue($"{totals.PowerGenerationModifier:0}");
            turningPower.SetValue($"{totals.TurningPowerModifier:0}");
        }
    }
}