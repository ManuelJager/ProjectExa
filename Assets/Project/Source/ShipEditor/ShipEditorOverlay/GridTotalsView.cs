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
        [SerializeField] private PropertyView energyView;
        [SerializeField] private PropertyView turningPower;

        public void Update()
        {
            var totals = Systems.ShipEditor.editorGrid.blueprintLayer.ActiveBlueprint.Blocks.Totals;
            Render(totals);
        }

        private void Render(GridTotals totals)
        {
            massView.SetValue($"{totals.Mass:0} KG");
            hullView.SetValue($"{totals.Hull:0}");
            energyView.SetValue($"{totals.PowerGenerationModifier:0}");
            turningPower.SetValue($"{totals.TurningPowerModifier:0}");
        }
    }
}