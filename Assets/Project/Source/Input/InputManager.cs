using Exa.UI;
using Exa.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Exa.Input
{
    public class InputManager : MonoSingleton<InputManager>
    {
        public bool InputIsCaptured;
        public MousePointer mousePointer;

        [SerializeField] private Canvas root;
        private bool mouseInViewport = false;

        public Vector2 ScaledMousePosition
        {
            get => Mouse.current.position.ReadValue() / root.scaleFactor;
        }

        protected override void Awake()
        {
            base.Awake();

            mousePointer.SetState(CursorState.idle);
        }

        private void Update()
        {
            var mousePos = Mouse.current.position.ReadValue();
            var posInViewport = Camera.main.ScreenToViewportPoint(mousePos);

            var currFrameMouseInViewport = !(
                posInViewport.x < 0f ||
                posInViewport.x > 1f ||
                posInViewport.y < 0f ||
                posInViewport.y > 1f);

            if (currFrameMouseInViewport != mouseInViewport)
            {
                mousePointer.gameObject.SetActive(currFrameMouseInViewport);
                mouseInViewport = currFrameMouseInViewport;
            }
        }

        public void OnHoverOverControl(CursorState desiredCursorStateByControl)
        {
            mousePointer.SetState(desiredCursorStateByControl);
        }

        public void OnExitControl()
        {
            mousePointer.SetState(CursorState.idle);
        }
    }
}