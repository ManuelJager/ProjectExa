using Exa.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Exa.Input
{
    public class InputManager : MonoBehaviour
    {
        [HideInInspector] public bool inputIsCaptured;

        private bool mouseInViewport = false;
        private MouseCursorController mouseCursor;
        private Canvas root;
        private Stack<Hoverable> hoverableStack = new Stack<Hoverable>();

        public Vector2 ScaledMousePosition
        {
            get => Mouse.current.position.ReadValue() / root.scaleFactor;
        }

        private void Awake()
        {
            mouseCursor = Systems.MainUI.mouseCursor;
            root = Systems.MainUI.root;
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
                mouseCursor.gameObject.SetActive(currFrameMouseInViewport);
                mouseInViewport = currFrameMouseInViewport;
            }
        }

        public void OnHoverOverControl(Hoverable hoverable)
        {
            mouseCursor.SetState(hoverable.cursorState);
            hoverableStack.Push(hoverable);
        }

        public void OnExitControl()
        {
            hoverableStack.Pop();

            mouseCursor.SetState(hoverableStack.Count == 0
                ? CursorState.idle
                : hoverableStack.Peek().cursorState);
        }
    }
}