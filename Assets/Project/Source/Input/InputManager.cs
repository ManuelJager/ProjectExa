using Exa.UI;
using System.Linq;
using System.Numerics;
using Exa.Math;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

namespace Exa.Input
{
    public class InputManager : MonoBehaviour
    {
        [HideInInspector] public bool inputIsCaptured;

        private MouseCursorController mouseCursor;
        private Canvas root;

        public Vector2 MouseScaledViewportPoint { get; private set; }
        public Vector2 MouseScreenPoint { get; private set; }
        public Vector2 MouseViewportPoint { get; private set; }
        public Vector2 MouseWorldPoint { get; private set; }
        public Vector2 MouseOffsetFromCentre { get; private set; }

        private void Awake() {
            mouseCursor = Systems.UI.mouseCursor;
            root = Systems.UI.rootCanvas;
        }

        private void Update() {
            var mousePos = Mouse.current.position.ReadValue();
            MouseScaledViewportPoint = mousePos / root.scaleFactor;
            MouseScreenPoint = mousePos;
            MouseWorldPoint = Camera.main.ScreenToWorldPoint(mousePos);
            MouseViewportPoint = Camera.main.ScreenToViewportPoint(mousePos);
            MouseOffsetFromCentre = CalculateMouseOffsetFromCentre(MouseViewportPoint);

            var currFrameMouseInViewport = !(
                MouseViewportPoint.x < 0f ||
                MouseViewportPoint.x > 1f ||
                MouseViewportPoint.y < 0f ||
                MouseViewportPoint.y > 1f);

            mouseCursor.UpdateMouseInViewport(currFrameMouseInViewport);
        }

        public bool GetMouseInsideRect(RectTransform rect) {
            return RectTransformUtility.RectangleContainsScreenPoint(rect, MouseScreenPoint, Camera.main);
        }

        public bool GetMouseInsideRect(params RectTransform[] rects) {
            return rects.Any(GetMouseInsideRect);
        }

        private Vector2 CalculateMouseOffsetFromCentre(Vector2 mouseViewportPoint) {
            var min = new Vector2(-1, -1);
            var max = new Vector2(1, 1);
            var unclampedOffset = mouseViewportPoint * 2f - max;
            return unclampedOffset.Clamp(min, max);
        }
    }
}