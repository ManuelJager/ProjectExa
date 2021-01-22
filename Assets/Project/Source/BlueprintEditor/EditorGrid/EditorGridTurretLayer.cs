using System.Collections;
using System.Collections.Generic;
using Exa.Grids;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blueprints;
using Exa.Math;
using Exa.Types;
using Exa.Types.Binding;
using Exa.Utils;
using UnityEngine;

namespace Exa.ShipEditor
{
    public class EditorGridTurretLayer : MonoBehaviour
    {
        [SerializeField] private GameObject basePrefab;
        [SerializeField] private Transform prefabContainer;
        [SerializeField] private Transform instanceContainer;

        private Dictionary<ITurretTemplate, GameObject> overlayPrefabs;
        private Dictionary<AnchoredBlueprintBlock, TurretOverlay> overlayInstances;
        private bool stationaryOverlayVisibility;

        public bool StationaryOverlayVisibility {
            set {
                stationaryOverlayVisibility = value;
                overlayInstances.Values.ForEach(overlay => overlay.gameObject.SetActive(value));
            }
        }

        public void Init() {
            prefabContainer.DestroyChildren();
            instanceContainer.DestroyChildren();
            overlayPrefabs = new Dictionary<ITurretTemplate, GameObject>();
            overlayInstances = new Dictionary<AnchoredBlueprintBlock, TurretOverlay>();
        }

        public TurretOverlay GetStationaryOverlay(AnchoredBlueprintBlock block) {
            return overlayInstances[block];
        }

        public TurretOverlay AddStationaryOverlay(AnchoredBlueprintBlock block, ITurretTemplate template) {
            var overlay = CreateOverlay(template, block, TurretOverlayMode.Stationary);
            overlay.SetColor(Color.white.SetAlpha(0.5f)); 
            overlay.Presenter.Present(block);
            overlay.gameObject.SetActive(stationaryOverlayVisibility);
            overlayInstances[block] = overlay;
            return overlay;
        }

        public TurretOverlay CreateGhostOverlay(AnchoredBlueprintBlock block, ITurretTemplate template) {
            return CreateOverlay(template, block, TurretOverlayMode.Ghost);
        }

        private TurretOverlay CreateOverlay(ITurretTemplate template, AnchoredBlueprintBlock block, TurretOverlayMode mode) {
            var overlay = overlayPrefabs[template].Create<TurretOverlay>(instanceContainer);
            overlay.Import(block, mode);
            return overlay;
        }

        public void RemoveStationaryOverlay(AnchoredBlueprintBlock block) {
            Destroy(overlayInstances[block].gameObject);
            overlayInstances.Remove(block);
        }

        // TODO: Don't hardcode the user group here
        public void GenerateTurretOverlayPrefab(ITurretTemplate template) {
            var overlay = basePrefab.Create<TurretOverlay>(prefabContainer);
            overlay.Generate(template.GetTurretValues(BlockContext.UserGroup));
            overlayPrefabs[template] = overlay.gameObject;
        }
    }
}
