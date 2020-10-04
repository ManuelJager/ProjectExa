using Exa.Audio;
using Exa.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using static Exa.Input.GameControls;
using static UnityEngine.InputSystem.InputAction;

namespace Exa.UI.Components
{
    public class ReturnNavigateable : Navigateable, IReturnNavigateableActions
    {
        public GameControls gameControls;

        [SerializeField] private GlobalAudioPlayerProxy audioPlayer;
        private Navigateable returnTarget = null;

        protected virtual void Awake()
        {
            gameControls = new GameControls();
            gameControls.ReturnNavigateable.SetCallbacks(this);
        }

        public override void HandleEnter(Navigateable from, NavigationArgs args = default)
        {
            if (!args.isReturning)
            {
                returnTarget = from;
            }

            base.HandleEnter(from, args);
        }

        protected virtual void Return(bool force = false)
        {
            if (!Interactable && !force) return;

            audioPlayer.Play("UI_SFX_MenuTransitionOut");
            NavigateTo(returnTarget, new NavigationArgs
            {
                isReturning = true
            });
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