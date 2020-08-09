using Exa.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using static Exa.Input.GameControls;

namespace Exa.Gameplay
{
    public partial class GameplayInputManager : MonoBehaviour, IGameplayActions
    {
        [SerializeField] private GameplayCameraController gameplayCameraController;
        private GameControls gameControls;

        public void Awake()
        {
            gameControls = new GameControls();
            gameControls.Gameplay.SetCallbacks(this);
        }

        public void Update()
        {
            UpdateRaycastTarget();

            if (IsSelectingArea)
            {
                OnUpdateSelectionArea();
            }
        }

        public void OnEnable()
        {
            gameControls.Enable();
        }

        public void OnDisable()
        {
            gameControls.Disable();
        }

        private void UpdateRaycastTarget()
        {
            void OnEnter(IRaycastTarget raycastTarget)
            {
                this.raycastTarget = raycastTarget;
                this.raycastTarget?.OnRaycastEnter();
            }

            void OnExit()
            {
                raycastTarget?.OnRaycastExit();
                raycastTarget = null;
            }

            var worldPoint = Systems.InputManager.MouseWorldPoint;
            var hits = Physics2D.RaycastAll(worldPoint, Vector2.zero);

            // TODO: Fix this as it doesn't actually support multiple rays
            var foundTarget = false;
            foreach (var hit in hits)
            {
                var go = hit.transform.gameObject;
                var raycastTarget = go.GetComponent<IRaycastTarget>();

                if (raycastTarget != null)
                {
                    foundTarget = true;

                    if (this.raycastTarget != raycastTarget)
                    {
                        OnExit();
                        OnEnter(raycastTarget);
                        return;
                    }
                }
            }

            if (!foundTarget)
            {
                OnExit();
            }
        }
    }
}