using DG.Tweening;
using Exa.Math;
using Exa.Utils;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Gameplay
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        private Camera targetCamera;

        private Tween cameraMoveTween;
        private Tween cameraZoomTween;

        public ICameraTarget CurrentTarget { get; private set; }
        public UserTarget UserTarget { get; } = new UserTarget();

        private void Awake() {
            targetCamera = Camera.main;
        }

        private void Update() {
            if (CurrentTarget != null && !CurrentTarget.TargetValid)
                EscapeTarget();

            if (CurrentTarget != null)
                UserTarget.ImportValues(CurrentTarget);
            else
                UserTarget.Tick();

            var target = GetTarget();
            var cameraOrthoSize = target.GetCalculatedOrthoSize();
            var cameraPosition = target.GetWorldPosition().ToVector3(-10);

            targetCamera.DOOrthoSize(cameraOrthoSize, 0.2f)
                .Replace(ref cameraZoomTween);
            targetCamera.transform.DOMove(cameraPosition, 0.2f)
                .Replace(ref cameraMoveTween);
        }

        public void SetTarget(ICameraTarget newTarget, bool teleport = false) {
            if (teleport) {
                targetCamera.transform.position = newTarget.GetWorldPosition().ToVector3(-10);
                targetCamera.orthographicSize = newTarget.GetCalculatedOrthoSize();
            }

            CurrentTarget = newTarget;
        }

        public void EscapeTarget() {
            CurrentTarget = null;
        }

        public ICameraTarget GetTarget() {
            return CurrentTarget ?? UserTarget;
        }
    }
}