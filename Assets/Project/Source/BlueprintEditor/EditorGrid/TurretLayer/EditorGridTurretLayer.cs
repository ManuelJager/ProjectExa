using System.Collections.Generic;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blueprints;
using Exa.Utils;
using NaughtyAttributes;
using UnityEngine;

namespace Exa.ShipEditor {
    public class EditorGridTurretLayer : CustomEditorGridLayer<ITurretTemplate> {
        [SerializeField] private GameObject basePrefab;
        [SerializeField] private Transform prefabContainer;
        [SerializeField] private Transform instanceContainer;
        private Dictionary<ABpBlock, TurretOverlay> overlayInstances;

        private Dictionary<ITurretTemplate, GameObject> overlayPrefabs;
        private bool overlayVisibility;

        public bool OverlayVisibility {
            set {
                overlayVisibility = value;
                overlayInstances.Values.ForEach(overlay => overlay.SetVisibility(value));
            }
        }

        public TurretBlocks TurretBlocks { get; private set; }

        public void Init() {
            prefabContainer.DestroyChildren();
            instanceContainer.DestroyChildren();
            TurretBlocks = new TurretBlocks(this);
            overlayPrefabs = new Dictionary<ITurretTemplate, GameObject>();
            overlayInstances = new Dictionary<ABpBlock, TurretOverlay>();

            var templates = S.Blocks.blockTemplates.SelectNonNull(elem => elem.Data as ITurretTemplate);

            foreach (var template in templates) {
                GenerateTurretOverlayPrefab(template);
            }
        }

        [Button]
        public void PrintTurretClaims() {
            Debug.Log(TurretBlocks.GetTurretClaims().Join(", "));
        }

        public TurretOverlay CreateGhostOverlay(ABpBlock block, ITurretTemplate template) {
            return CreateOverlay(block, template);
        }

        public TurretOverlay CreateStationaryOverlay(ABpBlock block, ITurretTemplate template) {
            var overlay = CreateOverlay(block, template);
            overlay.SetColor(Color.white.SetAlpha(0.5f));
            overlay.Presenter.Present(block);
            overlay.SetVisibility(overlayVisibility);
            overlayInstances[block] = overlay;

            return overlay;
        }

        private TurretOverlay CreateOverlay(ABpBlock block, ITurretTemplate template) {
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

        protected override void OnAdd(ABpBlock block, ITurretTemplate template) {
            TurretBlocks.AddTurret(block, template);
        }

        protected override void OnRemove(ABpBlock block, ITurretTemplate template) {
            TurretBlocks.Remove(block);
        }
    }
}