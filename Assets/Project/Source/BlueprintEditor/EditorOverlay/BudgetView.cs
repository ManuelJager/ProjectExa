using Exa.Grids.Blocks;
using UnityEngine;

namespace Exa.ShipEditor
{
    public class BudgetView : MonoBehaviour
    {
        public BlockCostsView view;

        public void SetBudget(BlockCosts budget) {
            view.Refresh(budget);
        }
    }
}