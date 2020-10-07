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

        [SerializeField] private GlobalAudioPlayerProxy _audioPlayer;
        private Navigateable _returnTarget = null;

        protected virtual void Awake()
        {
            gameControls = new GameControls();
            gameControls.ReturnNavigateable.SetCallbacks(this);
        }

        public override void HandleEnter(NavigationArgs args)
        {
            if (!args.isReturning)
            {
                _returnTarget = args.current;
            }

            base.HandleEnter(args);
        }

        protected virtual void Return(bool force = false)
        {
            if (!Interactable && !force) return;

            _audioPlayer.Play("UI_SFX_MenuTransitionOut");
            NavigateTo(_returnTarget, new NavigationArgs
            {
                current = this,
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