using UnityEngine;

namespace Exa.Debugging
{
    public class DebugDragger : MonoBehaviour
    {
        private IDebugDragable _currentDragable;
        private Vector2 _offset;
        private Vector2 _averagedVelocity;
        private float _sampleTime = 0.2f;

        public void Update()
        {
            if (_currentDragable == null) return;

            // Set the velocity
            var mouseWorldPoint = Systems.Input.MouseWorldPoint;
            var velocity = _currentDragable.GetPosition() - _offset - mouseWorldPoint;
            AverageVelocity(-velocity, _sampleTime);

            var targetPoint = mouseWorldPoint + _offset;
            _currentDragable.SetGlobals(targetPoint, Vector2.zero);
        }

        public void OnPress()
        {
            if (!TryGetDebugDragable(out var dragable)) return;

            _currentDragable = dragable;
            _averagedVelocity = Vector2.zero;

            var mouseWorldPoint = Systems.Input.MouseWorldPoint;
            var currentPoint = _currentDragable.GetPosition();

            _offset = currentPoint - mouseWorldPoint;
            _currentDragable.SetGlobals(currentPoint, Vector2.zero);
        }

        public void OnRelease()
        {
            if (_currentDragable == null) return;

            var mouseWorldPoint = Systems.Input.MouseWorldPoint;
            var targetPoint = mouseWorldPoint + _offset;
            _currentDragable.SetGlobals(targetPoint, _averagedVelocity);
            _currentDragable = null;
        }

        private void AverageVelocity(Vector2 newVelocity, float sampleTime)
        {
            var deltaTime = Time.deltaTime;
            var total = _averagedVelocity * deltaTime * (sampleTime - deltaTime) + newVelocity;
            _averagedVelocity = total * sampleTime / deltaTime;
        }

        private static bool TryGetDebugDragable(out IDebugDragable dragable)
        {
            dragable = null;

            try
            {
                var raycastTarget = GameSystems.GameplayInputManager.RaycastTarget;
                var raycastTargetIsDragable = raycastTarget is IDebugDragable;

                if (raycastTargetIsDragable)
                {
                    dragable = raycastTarget as IDebugDragable;
                }

                return raycastTargetIsDragable;
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch
            {
                return false;
            }
        }
    }
}

