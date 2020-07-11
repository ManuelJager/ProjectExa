using Exa.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using static Exa.Input.GameControls;

namespace Exa.Gameplay
{
    public class GameplayInputController : MonoBehaviour, IGameplayActions
    {
        [SerializeField] private GameplayCameraController gameplayCameraController;
        private GameControls gameControls;

        public void Awake()
        {
            gameControls = new GameControls();
            gameControls.Gameplay.SetCallbacks(this);
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
    }
}
