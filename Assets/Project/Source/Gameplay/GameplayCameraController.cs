using DG.Tweening;
using Exa.Math;
using Exa.Utils;
using UnityEngine;

namespace Exa.Gameplay
{
    public class GameplayCameraController : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;
        private Camera _target;
        private readonly UserCameraTarget _userCameraTarget = new UserCameraTarget();
        private ICameraTarget _currentTarget;
        private Vector2 _movementDelta;

        private bool IsTargetUserControlled => _userCameraTarget == _currentTarget;

        private void Awake()
        {
            _target = Camera.main;
            SetTarget(_userCameraTarget);
        }

        private void Update()
        {
            _userCameraTarget.worldPosition += _movementDelta * _movementSpeed * Time.deltaTime;

            if (_currentTarget != null)
            {
                var targetOrthoSize = _currentTarget.GetOrthoSize();
                var targetPosition = _currentTarget.GetWorldPosition().ToVector3(-10);
                _target.transform.DOMove(targetPosition, 0.2f);
                _target.DOOrthoSize(targetOrthoSize, 0.2f);
            }
        }

        public void SetTarget(ICameraTarget cameraTarget)
        {
            _currentTarget = cameraTarget;
        }

        public void SetMovementDelta(Vector2 delta)
        {
            _movementDelta = IsTargetUserControlled
                ? delta
                : Vector2.zero;
        }
    }
}