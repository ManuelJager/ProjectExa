using Exa.Math;
using Exa.Ships.Rotation;
using Exa.Utils;
using UnityEngine;
using UnityEngine.Serialization;
using Vectrosity;

namespace Exa.UI.Gameplay {
    public class RotationOverlay : MonoBehaviour {
        [SerializeField] private float worldUnitLength;
        
        public RotationController Rc { get; set; }
        [SerializeField] private VectorObject2D targetRotation;
        [SerializeField] private VectorObject2D currRotation;
        
        private UnityEngine.Camera cam;
        private VectorLine targetLine;
        private VectorLine currLine;
        
        private void Awake() {
            cam = UnityEngine.Camera.main;
            targetLine = targetRotation.vectorLine;
            currLine = currRotation.vectorLine;
        }

        public void Update() {
            if (Rc != null) {
                var pos = Rc.Rb.worldCenterOfMass;
                var endPos = (Vector2.right * worldUnitLength).Rotate(Rc.Rb.rotation) + pos;
                
                currLine.points2[0] = ScreenPoint(pos);
                currLine.points2[1] = ScreenPoint(endPos);
                currLine.Draw();

                if (Rc.TargetRotation.GetHasValue(out var value)) {
                    var endTargetPos = (Vector2.right * worldUnitLength).Rotate(value) + pos;
                    
                    targetLine.points2[0] = ScreenPoint(pos);
                    targetLine.points2[1] = ScreenPoint(endTargetPos);
                    targetLine.Draw();
                }
            }
        }

        private Vector2 ScreenPoint(Vector2 worldPoint) {
            return cam.WorldToScreenPoint(worldPoint);
        }
    }
}