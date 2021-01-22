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
using NaughtyAttributes;
using UnityEngine;

namespace Exa.ShipEditor
{
    public class EditorGridTurretLayer : MonoBehaviour
    {
        [SerializeField] private GameObject basePrefab;
        [SerializeField] private Transform prefabContainer;
        [SerializeField] private Transform instanceContainer;

        private Dictionary<ITurretTemplate, GameObject> overlayPrefabs;
        private Dictionary<ABpBlock, TurretOverlay> overlayInstances;
        private bool stationaryOverlayVisibility;

        public bool StationaryOverlayVisibility {
            set {
                stationaryOverlayVisibility = value;
                overlayInstances.Values.ForEach(overlay => overlay.gameObject.SetActive(value));
            }
        }

        public TurretBlocks TurretBlocks { get; private set; }

        public void Init() {
            prefabContainer.DestroyChildren();
            instanceContainer.DestroyChildren();
            TurretBlocks = new TurretBlocks(this);
            overlayPrefabs = new Dictionary<ITurretTemplate, GameObject>();
            overlayInstances = new Dictionary<ABpBlock, TurretOverlay>();
        }

        [Button]
        public void PrintTurretClaims() {
            Debug.Log(TurretBlocks.GetTurretClaims().Join(", "));
        }

        public TurretOverlay GetStationaryOverlay(ABpBlock block) {
            return overlayInstances[block];
        }

        public TurretOverlay AddStationaryOverlay(ABpBlock block, ITurretTemplate template) {
            var overlay = CreateOverlay(template, block);
            overlay.SetColor(Color.white.SetAlpha(0.5f)); 
            overlay.Presenter.Present(block);
            overlay.gameObject.SetActive(stationaryOverlayVisibility);
            overlayInstances[block] = overlay;
            return overlay;
        }

        public TurretOverlay CreateGhostOverlay(ABpBlock block, ITurretTemplate template) {
            return CreateOverlay(template, block);
        }

        private TurretOverlay CreateOverlay(ITurretTemplate template, ABpBlock block) {
            var overlay = overlayPrefabs[template].Create<TurretOverlay>(instanceContainer);
            overlay.Import(block);
            return overlay;
        }

        public void RemoveStationaryOverlay(ABpBlock block) {
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
