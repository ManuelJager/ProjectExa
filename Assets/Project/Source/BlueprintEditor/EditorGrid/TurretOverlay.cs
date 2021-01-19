using System.Collections.Generic;
using System.Linq;
using Exa.Grids;
using Exa.Grids.Blocks.Components;
using Exa.Math;
using Exa.UI.Tweening;
using Exa.Utils;
using UnityEngine;

namespace Exa.ShipEditor
{
    public class TurretOverlay : MonoBehaviour
    {
        [SerializeField] private BlockPresenter presenter;
        [SerializeField] private MeshCollider meshCollider;
        [SerializeField] private ExaEase ease;

        private TurretOverlayHandle handle;

        public BlockPresenter Presenter => presenter;

        public Color Color {
            set => presenter.Renderer.color = value;
        }

        public void Generate(ITurretValues values) {
            presenter.Renderer.sprite = GenerateTexture(values).CreateSprite();
            meshCollider.sharedMesh = GenerateMesh(values);
            gameObject.SetActive(false);
        }

        public void ConfigureAsGhostOverlay(TurretOverlayHandle handle) {
            this.handle = handle;
            meshCollider.isTrigger = true;
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

        private Mesh GenerateMesh(ITurretValues values, int subdivisions = 20) {
            var mesh = new Mesh();
            var vertices = new List<Vector3>(subdivisions + 2) { Vector3.zero };
            var triangles = new List<int>(subdivisions * 3);

            void AddVertex(float angle) {
                vertices.Add(MathUtils.FromAngledMagnitude(values.TurretRadius, angle));
            }

            var startAngle = -(values.TurretArc / 2f);
            var subdivisionAngleSize = values.TurretArc / subdivisions;

            AddVertex(startAngle);
            for (var index = 0; index < subdivisions; index ++) {
                var angleOffset = subdivisionAngleSize * (index + 1);
                AddVertex(startAngle + angleOffset);
                triangles.AddRange(0, index + 1, index + 2);
            }

            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();

            return mesh;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            Debug.Log("Entered");
            handle.Collides = true;
        }

        private void OnTriggerExit2D(Collider2D other) {
            Debug.Log("Exited");
            handle.Collides = false;
        }
    }
}