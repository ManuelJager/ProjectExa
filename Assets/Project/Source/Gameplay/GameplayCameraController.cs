using DG.Tweening;
using Exa.Utils;
using UnityEngine;

namespace Exa.Gameplay
{
    public class GameplayCameraController : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        private Camera target;
        private UserCameraTarget userCameraTarget = new UserCameraTarget();
        private ICameraTarget currentTarget;
        private Vector2 movementDelta;

        private bool IsTargetUserControlled => userCameraTarget == currentTarget;

        private void Awake()
        {
            target = Camera.main;
            SetTarget(userCameraTarget);
        }

        private void Update()
        {
            userCameraTarget.worldPosition += movementDelta * movementSpeed * Time.deltaTime;

            if (currentTarget != null)
            {
                var targetOrthoSize = currentTarget.GetOrthoSize();
                var targetPosition = currentTarget.GetWorldPosition().ToVector3(-10);
                target.transform.DOMove(targetPosition, 0.2f);
                target.DOOrthoSize(targetOrthoSize, 0.2f);
            }
        }

        public void SetTarget(ICameraTarget cameraTarget)
        {
            currentTarget = cameraTarget;
        }

        public void SetMovementDelta(Vector2 delta)
        {
            movementDelta = IsTargetUserControlled
                ? delta
                : Vector2.zero;
        }
    }
}