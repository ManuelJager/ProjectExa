using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Grids;
using Exa.Grids.Blocks.Components;
using Exa.Grids.Blueprints;
using Exa.Math;
using Exa.UI.Tweening;
using Exa.Utils;
using NaughtyAttributes;
using UnityEngine;

namespace Exa.ShipEditor
{
    public enum TurretOverlayMode
    {
        Stationary,
        Ghost
    }

    public class TurretOverlay : MonoBehaviour
    {
        [SerializeField] private BlockPresenter presenter;
        [SerializeField] private PolygonCollider2D polygonCollider;
        [SerializeField] private ExaEase ease;
        private TurretOverlayMode mode;

        public BlockPresenter Presenter => presenter;
        public AnchoredBlueprintBlock Block { get; private set; }

        public Color Color {
            set => presenter.Renderer.color = value;
        }

        private void Start() {
            if (mode == TurretOverlayMode.Stationary) {
                Systems.Editor.editorGrid.blueprintLayer.OnTurretOverlayStart(this);
            }
        }

        public void Generate(ITurretValues values) {
            presenter.Renderer.sprite = GenerateTexture(values).CreateSprite();
            polygonCollider.points = GeneratePoints(values);
            gameObject.SetActive(false);
        }

        public void SetMode(AnchoredBlueprintBlock block, TurretOverlayMode mode) {
            this.Block = block;
            this.mode = mode;
        }

        public IEnumerable<Vector2Int> GetContacts() {
            return polygonCollider.StationaryCast(LayerMask.GetMask("editorGridItems"))
                .Select(hit => hit.transform.GetComponent<EditorGridItem>().GridPosition)
                .Except(Block.GetOccupiedTiles());
        }

        private Texture2D GenerateTexture(ITurretValues values) {
            var pixelRadius = Mathf.RoundToInt(values.TurretRadius * 32);
            var size = pixelRadius * 2;
            var centre = (pixelRadius - 0.5f).ToVector2();
            var arc = values.TurretArc;
            return new Texture2D(size, size).SetDefaults()
                .DrawCone(Color.white.SetAlpha(1f), centre, pixelRadius, arc)
                .DrawFadingCone(Color.white.SetAlpha(0.5f), centre, pixelRadius - 1.2f, arc, ease.Evaluate);
        }

        private Vector2[] GeneratePoints(ITurretValues values, int subdivisions = 20) {
            var points = new Vector2[subdivisions + 2];
            points[0] = Vector2.zero;

            var startAngle = -(values.TurretArc / 2f);
            var subdivisionAngleSize = values.TurretArc / subdivisions;

            for (var i = 0; i < subdivisions + 1; i++) {
                var angle = startAngle + subdivisionAngleSize * i;
                points[i + 1] = MathUtils.FromAngledMagnitude(values.TurretRadius, angle);
            }

            return points;
        }
    }
}