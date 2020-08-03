using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using UnityEngine;

namespace Exa.Grids
{
    public class ShipGrid : MonoBehaviour
    {
        protected Blueprint blueprint;

        public void Import(Blueprint blueprint)
        {
            this.blueprint = blueprint;

            foreach (var anchoredBlueprintBlock in blueprint.Blocks.GridMembers)
            {
                var block = anchoredBlueprintBlock.CreateBehaviourInGrid(transform, BlockPrefabType.userGroup);
                block.shipGrid = this;
                block.gameObject.SetActive(true);
            }

            UpdateCOMPivot();
        }

        private void UpdateCOMPivot()
        {
            var COM = blueprint.Blocks.CentreOfMass.Value;
            transform.localPosition = -COM;
        }
    }
}