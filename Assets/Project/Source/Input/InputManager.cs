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

        public Vector2 ScaledMousePosition
        {
            get => Mouse.current.position.ReadValue() / root.scaleFactor;
        }

        public Vector2 MouseWorldPoint { get; private set; }

        private void Awake()
        {
            mouseCursor = Systems.MainUI.mouseCursor;
            root = Systems.MainUI.root;
        }

        private void Update()
        {
            var mousePos = Mouse.current.position.ReadValue();
            MouseWorldPoint = Camera.main.ScreenToWorldPoint(mousePos);
            var posInViewport = Camera.main.ScreenToViewportPoint(mousePos);

            var currFrameMouseInViewport = !(
                posInViewport.x < 0f ||
                posInViewport.x > 1f ||
                posInViewport.y < 0f ||
                posInViewport.y > 1f);

            if (currFrameMouseInViewport != mouseInViewport)
            {
                mouseInViewport = currFrameMouseInViewport;
                mouseCursor.SetMouseInViewport(mouseInViewport);
            }
        }
    }
}