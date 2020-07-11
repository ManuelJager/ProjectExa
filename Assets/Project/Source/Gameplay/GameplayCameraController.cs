using UnityEngine;
using DG.Tweening;

namespace Exa.Gameplay
{
    public class GameplayCameraController : MonoBehaviour
    {
        private Camera target;
        private UserCameraTarget userCameraTarget = new UserCameraTarget();
        private ICameraTarget currentTarget;
        private Vector2 movementDelta;

        private bool IsTargetUserControlled => userCameraTarget == currentTarget;

        private void Awake()
        {
            target = Camera.main;
            SetUserTarget();
        }

        private void Update()
        {
            userCameraTarget.worldPosition += movementDelta * Time.deltaTime;

            if (currentTarget != null)
            {
                target.transform.DOMove(currentTarget.GetWorldPosition(), 0.2f);
            }
        }

        public void SetTarget(ICameraTarget cameraTarget)
        {
            currentTarget = cameraTarget;
        }

        public void SetUserTarget()
        {
            SetTarget(userCameraTarget);
        }

        public void SetMovementDelta(Vector2 delta)
        {
            movementDelta = IsTargetUserControlled
                ? delta 
                : Vector2.zero;
        }
    }
}