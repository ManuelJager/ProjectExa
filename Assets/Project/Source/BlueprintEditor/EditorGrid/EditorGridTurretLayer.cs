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

        public void AddStationaryOverlay(AnchoredBlueprintBlock block, ITurretTemplate template) {
            var overlay = overlayPrefabs[template].InstantiateAndGet<TurretOverlay>(instanceContainer);
            overlay.transform.localPosition = block.GetLocalPosition();
            overlay.Color = Color.white.SetAlpha(0.5f);
            overlay.gameObject.SetActive(stationaryOverlayVisibility);
            overlayInstances[block] = overlay;
        }

        public void RemoveStationaryOverlay(AnchoredBlueprintBlock block) {
            Destroy(overlayInstances[block].gameObject);
            overlayInstances.Remove(block);
        }

        public void GenerateTurretOverlayPrefab(ITurretTemplate template) {
            var overlay = basePrefab.InstantiateAndGet<TurretOverlay>(prefabContainer);
            overlay.Import(template);
            overlayPrefabs[template] = overlay.gameObject;
        }
    }
}
