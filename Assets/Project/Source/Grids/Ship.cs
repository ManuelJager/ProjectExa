using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blueprints;
using Exa.Utils;
using UnityEngine;

namespace Exa.Grids
{
    public class Ship : MonoBehaviour
    {
        public BlockGrid blockGrid;

        [SerializeField] protected Transform pivot;
        protected Blueprint blueprint;

        private void LateUpdate()
        {
            UpdateCentreOfMassPivot(true);
        }

        private void Awake()
        {
            blockGrid = new BlockGrid(pivot);
        }

        public virtual void Import(Blueprint blueprint)
        {
            foreach (var anchoredBlueprintBlock in blueprint.Blocks.GridMembers)
            {
                blockGrid.Add(CreateBlock(anchoredBlueprintBlock));
            }

            this.blueprint = blueprint;
            UpdateCentreOfMassPivot(false);
        }


        private Block CreateBlock(AnchoredBlueprintBlock anchoredBlueprintBlock)
        {
            var block = anchoredBlueprintBlock.CreateBehaviourInGrid(pivot, BlockPrefabType.userGroup);
            block.ship = this;
            block.gameObject.SetActive(true);
            return block;
        }

        private void UpdateCentreOfMassPivot(bool updateSelf)
        {
            var COMOffset = -blockGrid.CentreOfMass.Value;

            if (updateSelf)
            {
                var currentPosition = pivot.localPosition.ToVector2();
                var diff = currentPosition - COMOffset;
                transform.localPosition += diff.ToVector3();
            }

            pivot.localPosition = COMOffset;
        }
    }
}