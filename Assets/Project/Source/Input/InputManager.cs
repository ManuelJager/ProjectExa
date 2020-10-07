using Exa.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Exa.Input
{
    public class InputManager : MonoBehaviour
    {
        [HideInInspector] public bool inputIsCaptured;

        private bool _mouseInViewport = false;
        private MouseCursorController _mouseCursor;
        private Canvas _root;

        public Vector2 ScaledViewportPoint { get; private set; }

        public Vector2 ScreenPoint { get; private set; }

        public Vector2 ViewportPoint { get; private set; }

        public Vector2 MouseWorldPoint { get; private set; }

        private void Awake()
        {
            _mouseCursor = Systems.Ui.mouseCursor;
            _root = Systems.Ui.root;
        }

        private void Update()
        {
            var mousePos = Mouse.current.position.ReadValue();
            ScaledViewportPoint = mousePos / _root.scaleFactor;
            ScreenPoint = mousePos;
            MouseWorldPoint = Camera.main.ScreenToWorldPoint(mousePos);
            ViewportPoint = Camera.main.ScreenToViewportPoint(mousePos);

            var currFrameMouseInViewport = !(
                ViewportPoint.x < 0f ||
                ViewportPoint.x > 1f ||
                ViewportPoint.y < 0f ||
                ViewportPoint.y > 1f);

            if (currFrameMouseInViewport != _mouseInViewport)
            {
                _mouseInViewport = currFrameMouseInViewport;
                _mouseCursor.SetMouseInViewport(_mouseInViewport);
            }
        }

        public bool GetMouseInsideRect(RectTransform rect)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(rect, ScreenPoint, Camera.main);
        }

        public bool GetMouseInsideRect(params RectTransform[] rects)
        {
            return rects.Any(GetMouseInsideRect);
        }
    }
}