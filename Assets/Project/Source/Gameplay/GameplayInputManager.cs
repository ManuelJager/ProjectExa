using Exa.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using static Exa.Input.GameControls;
#pragma warning disable 649

namespace Exa.Gameplay
{
    public partial class GameplayInputManager : MonoBehaviour, IGameplayActions
    {
        [SerializeField] private GameplayCameraController _gameplayCameraController;
        private GameControls _gameControls;

        public IRaycastTarget RaycastTarget => _raycastTarget;

        public void Awake()
        {
            _gameControls = new GameControls();
            _gameControls.Gameplay.SetCallbacks(this);
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
            _gameControls.Enable();
        }

        public void OnDisable()
        {
            _gameControls.Disable();
        }

        private void UpdateRaycastTarget()
        {
            void OnEnter(IRaycastTarget raycastTarget)
            {
                this._raycastTarget = raycastTarget;
                this._raycastTarget?.OnRaycastEnter();
            }

            void OnExit()
            {
                _raycastTarget?.OnRaycastExit();
                _raycastTarget = null;
            }

            var worldPoint = Systems.Input.MouseWorldPoint;
            var hits = Physics2D.RaycastAll(worldPoint, Vector2.zero);

            // TODO: Fix this as it doesn't actually support multiple rays
            var foundTarget = false;
            foreach (var hit in hits)
            {
                var go = hit.transform.gameObject;
                var raycastTarget = go.GetComponent<IRaycastTarget>();

                if (raycastTarget == null) continue;

                foundTarget = true;

                if (this._raycastTarget == raycastTarget) continue;

                OnExit();
                OnEnter(raycastTarget);
                return;
            }

            if (!foundTarget)
            {
                OnExit();
            }
        }
    }
}