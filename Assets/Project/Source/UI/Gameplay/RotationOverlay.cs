using Exa.Math;
using Exa.Ships.Rotation;
using Exa.Types.Generics;
using Exa.Utils;
using UnityEngine;
using UnityEngine.Serialization;
using Vectrosity;

namespace Exa.UI.Gameplay {
    public class RotationOverlay : MonoBehaviour {
        [Header("References")]
        [SerializeField] private VectorObject2D targetRotation;
        [SerializeField] private VectorObject2D currRotation;
        
        [Header("Settings")]
        [SerializeField] private MinMax<float> worldUnit;
        [SerializeField] private float sizeMultiplier;
        
        private UnityEngine.Camera cam;
        private VectorLine targetLine;
        private VectorLine currLine;
        private RotationController rc;

        public RotationController Rc {
            get => rc;
            set {
                rc = value;
                
                gameObject.SetActive(value != null);
            }
        }
        
        private void Awake() {
            cam = UnityEngine.Camera.main;
            targetLine = targetRotation.vectorLine;
            currLine = currRotation.vectorLine;
        }

        public void Update() {
            var lineWith = 1f / cam.orthographicSize * sizeMultiplier;
            
            currLine.lineWidth = lineWith;
            targetLine.lineWidth = lineWith;

            var currRot = Rc.Rb.rotation;
            var pivot = Rc.Rb.worldCenterOfMass;
            
            var startPos = MathUtils.FromAngledMagnitude(worldUnit.min, currRot) + pivot;
            var endPos = MathUtils.FromAngledMagnitude(worldUnit.max, currRot) + pivot;
            DrawLine(currLine, startPos, endPos);    
            
            var isRotatingTowardsTarget = Rc.TargetRotation.GetHasValue(out var targetRot);
            targetRotation.enabled = isRotatingTowardsTarget;
            
            if (isRotatingTowardsTarget) {
                var startTargetPos = MathUtils.FromAngledMagnitude(worldUnit.min, targetRot) + pivot;
                var endTargetPos = MathUtils.FromAngledMagnitude(worldUnit.max, targetRot) + pivot;
                targetLine.color = GetTargetLineColor(currRot, targetRot);
                DrawLine(targetLine, startTargetPos, endTargetPos);
            }
        }

        private Color GetTargetLineColor(float currRot, float targetRot) {
            var delta = Mathf.Abs(Mathf.DeltaAngle(currRot, targetRot));

            return Colors.Verdigris.SetAlpha(delta > 15f ? 1f : delta / 15f);
        }

        private void DrawLine(VectorLine line, Vector2 start, Vector2 end) {
            line.points2[0] = ScreenPoint(start);
            line.points2[1] = ScreenPoint(end);
            line.Draw();
        }

        private Vector2 ScreenPoint(Vector2 worldPoint) {
            return cam.WorldToScreenPoint(worldPoint);
        }
    }
}