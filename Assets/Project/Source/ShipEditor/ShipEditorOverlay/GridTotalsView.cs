using Exa.Generics;
using Exa.Grids;
using Exa.UI.Tooltips;
using UnityEngine;

namespace Exa.ShipEditor
{
    public class GridTotalsView : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PropertyView _massView;
        [SerializeField] private PropertyView _hullView;
        [SerializeField] private PropertyView _energyView;
        [SerializeField] private PropertyView _turningPower;

        public void Update()
        {
            var totals = Systems.ShipEditor.editorGrid.blueprintLayer.ActiveBlueprint.Blocks.Totals;
            Render(totals);
        }

        private void Render(GridTotals totals)
        {
            _massView.SetValue($"{totals.Mass:0} Tonne");
            _hullView.SetValue($"{totals.Hull:0}");
            _energyView.SetValue($"{totals.PowerGenerationModifier:0}");
            _turningPower.SetValue($"{totals.TurningPowerModifier:0}");
        }
    }
}