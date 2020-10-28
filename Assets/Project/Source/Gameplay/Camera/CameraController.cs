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
        private UserTarget userTarget = new UserTarget();
        private ICameraTarget currentTarget;

        private Tween cameraMoveTween;
        private Tween cameraZoomTween;

        public ICameraTarget CurrentTarget => currentTarget;

        private void Awake() {
            targetCamera = Camera.main;
        }

        private void Update() {
            if (GetTargetIsUserControlled(out var target)) {
                target.Tick();
            }

            if (currentTarget != null) {
                if (!currentTarget.GetTargetValid())
                    EscapeTarget();

                var targetOrthoSize = currentTarget.GetOrthoSize();
                var targetPosition = currentTarget.GetWorldPosition().ToVector3(-10);
                targetCamera.transform.DOMove(targetPosition, 0.2f)
                    .Replace(ref cameraMoveTween);
                targetCamera.DOOrthoSize(targetOrthoSize, 0.2f)
                    .Replace(ref cameraZoomTween);
            }
        }

        public void SetTarget(ICameraTarget newTarget) {
            if (currentTarget == null) {
                targetCamera.transform.position = newTarget.GetWorldPosition().ToVector3(-10);
                targetCamera.orthographicSize = newTarget.GetOrthoSize();
            }

            currentTarget = newTarget;
        }

        public bool GetTargetIsUserControlled(out UserTarget userTarget) {
            userTarget = currentTarget as UserTarget;
            return currentTarget is UserTarget;
        }

        public void EscapeTarget()
        {
            if (currentTarget.GetTargetValid())
                userTarget.ImportValues(currentTarget);

            SetTarget(userTarget);
        }
    }
}