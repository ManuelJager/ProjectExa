using Exa.Gameplay;
using Exa.Grids.Blueprints;
using Exa.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace Exa.Grids.Ships
{
    public class Ship : MonoBehaviour
    {
        public BlockGrid blockGrid;
        public ShipReferences references;
        public ShipOverlay overlay;
        public UnityEvent destroyEvent = new UnityEvent();

        protected Blueprint blueprint;
        private float hull;

        public Blueprint Blueprint => blueprint;

        public float Hull
        {
            get => hull;
            set
            {
                hull = value;
                overlay.SetFill(value);
            }
        }

        protected virtual void Awake()
        {
            blockGrid = new BlockGrid(references.pivot, this);
        }

        private void LateUpdate()
        {
            Hull = blockGrid.CurrentHull / blockGrid.TotalHull;
            UpdateCentreOfMassPivot(true);
        }

        public virtual void Import(Blueprint blueprint)
        {
            blockGrid.Import(blueprint);
            this.blueprint = blueprint;

            UpdateCanvasSize(blueprint);
            UpdateCentreOfMassPivot(false);
        }

        public string GetInstanceString()
        {
            return $"{blueprint.name} : {gameObject.GetInstanceID()}";
        }

        private void UpdateCentreOfMassPivot(bool updateSelf)
        {
            var COMOffset = -blockGrid.CentreOfMass.GetCentreOfMass();

            if (updateSelf)
            {
                var currentPosition = references.pivot.localPosition.ToVector2();
                var diff = currentPosition - COMOffset;
                transform.localPosition += diff.ToVector3();
            }

            references.pivot.localPosition = COMOffset;
        }

        private void UpdateCanvasSize(Blueprint blueprint)
        {
            var scale = blueprint.Blocks.MaxSize * 10 * references.canvasScaleMultiplier;
            overlay.rectContainer.sizeDelta = new Vector2(scale, scale);
        }
    }
}