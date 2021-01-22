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

        public BlockPresenter Presenter => presenter;
        public ABpBlock Block { get; private set; }

        public void SetColor(Color color) {
            presenter.Renderer.color = color;
        }

        public void Generate(ITurretValues values) {
            presenter.Renderer.sprite = GenerateTexture(values).CreateSprite();
            polygonCollider.points = GeneratePoints(values);
            gameObject.SetActive(false);
        }

        public void Import(ABpBlock block) {
            this.Block = block;
        }

        public IEnumerable<Vector2Int> GetTurretClaims() {
            return polygonCollider.StationaryCast(LayerMask.GetMask("editorGridItems"))
                .Select(hit => hit.transform.GetComponent<EditorGridItem>().GridPosition)
                .Except(Block.GetTileClaims());
        }

        [Button("Print turret claims")]
        public void PrintTurretClaims() {
            Debug.Log(GetTurretClaims().Join(", "));
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