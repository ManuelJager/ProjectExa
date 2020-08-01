using Exa.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using static Exa.Input.GameControls;

namespace Exa.Gameplay
{
    public class GameplayInputController : MonoBehaviour, IGameplayActions
    {
        [SerializeField] private GameplayCameraController gameplayCameraController;
        private GameControls gameControls;
        private IRaycastTarget currentRaycastTarget = null;

        public void Awake()
        {
            gameControls = new GameControls();
            gameControls.Gameplay.SetCallbacks(this);
        }

        public void Update()
        {
            UpdateRaycastTarget();
        }

        public void OnEnable()
        {
            gameControls.Enable();
        }

        public void OnDisable()
        {
            gameControls.Disable();
        }

        public void OnLeftClick(InputAction.CallbackContext context)
        {
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    gameplayCameraController.SetMovementDelta(context.ReadValue<Vector2>());
                    break;

                case InputActionPhase.Canceled:
                    gameplayCameraController.SetMovementDelta(Vector2.zero);
                    break;

                default:
                    return;
            }
        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
        }

        public void OnZoom(InputAction.CallbackContext context)
        {
        }

        private void UpdateRaycastTarget()
        {
            void OnEnter(IRaycastTarget raycastTarget)
            {
                currentRaycastTarget = raycastTarget;
                currentRaycastTarget?.OnRaycastEnter();
            }

            void OnExit()
            {
                currentRaycastTarget?.OnRaycastExit();
                currentRaycastTarget = null;
            }

            var mousePos = Mouse.current.position.ReadValue();
            var worldPoint = Camera.main.ScreenToWorldPoint(mousePos);

            var hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.transform != null)
            {
                var go = hit.transform.gameObject;
                var raycastTarget = go.GetComponent<IRaycastTarget>();

                if (currentRaycastTarget != raycastTarget)
                {
                    if (raycastTarget == null) OnExit();
                    else OnEnter(raycastTarget);
                }
            }
            else OnExit();
        }
    }
}