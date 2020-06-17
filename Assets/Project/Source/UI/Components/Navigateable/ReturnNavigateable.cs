using Exa.Input;
using UnityEngine.InputSystem;
using static Exa.Input.GameControls;
using static UnityEngine.InputSystem.InputAction;

namespace Exa.UI.Components
{
    public class ReturnNavigateable : Navigateable, IReturnNavigateableActions
    {
        public GameControls gameControls;

        private Navigateable from = null;

        protected virtual void Awake()
        {
            gameControls = new GameControls();
            gameControls.ReturnNavigateable.SetCallbacks(this);
        }

        public override void OnNavigate(Navigateable from, bool storeFrom = true)
        {
            if (storeFrom)
            {
                this.from = from;
            }
            base.OnNavigate(from, storeFrom);
        }

        protected virtual void Return(bool force = false)
        {
            if (!Interactable && !force) return;

            NavigateTo(from, false);
        }

        public void OnReturn(CallbackContext context)
        {
            if (context.phase != InputActionPhase.Started) return;

            Return();
        }

        protected virtual void OnEnable()
        {
            gameControls.Enable();
        }

        protected virtual void OnDisable()
        {
            gameControls.Disable();
        }
    }
}