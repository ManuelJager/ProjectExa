using System;
using Exa.UI.Tweening;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0649

namespace Exa.UI.Cursor {
    public class VirtualMouseCursor : MonoBehaviour, ICursor {
        [Header("References")]
        [SerializeField] internal Image backgroundImage;
        [SerializeField] internal CanvasGroup backgroundGroup;
        [SerializeField] internal RectTransform rectTransform;
        [SerializeField] internal RectTransform normalCursor;
        [SerializeField] internal RectTransform inputCursor;

        [Header("Facades")]
        [SerializeField] private StandardCursorFacade standardCursorFacade;
        
        private VirtualCursorFacade currentFacade;

        public CursorFacades CursorFacades { get; private set; }
        
        private void Update() {
            var viewportPoint = S.Input.MouseScaledViewportPoint;
            rectTransform.anchoredPosition = viewportPoint;
            currentFacade.Update(viewportPoint);
        }

        private void OnEnable() {
            CursorFacades = new CursorFacades(standardCursorFacade, SetFacade);
            SetFacade(standardCursorFacade);
        }

        private void OnDisable() {
            CursorFacades = null;
            SetFacade(null);
        }

        public void SetFacade(VirtualCursorFacade facade) {
            currentFacade?.OnDisable();
            currentFacade = facade;
            currentFacade?.OnEnable();
        }

        public void SetActive(bool active) {
            gameObject.SetActive(active);
        }

        public void SetState(CursorState cursorState) {
            standardCursorFacade.SetState(cursorState);
        }

        public void OnEnterViewport() {
            gameObject.SetActive(true);
            UnityEngine.Cursor.visible = false;
        }

        public void OnExitViewport() {
            gameObject.SetActive(false);
            UnityEngine.Cursor.visible = true;
        }

        public void Init(CursorStateOverrideList overrides) {
            standardCursorFacade.Init(this, overrides);
        }
    }
    
    public enum CursorDragState {
        NotDragging,
        BeginDragging,
        Dragging
    }

    [Serializable]
    public struct CursorSizeAnimSettings {
        public float sizeTarget;
        public float alphaTarget;
        public float animTime;
        public ExaEase ease;
    }

    [Serializable]
    public struct CursorDragAnimSettings {
        public float animTime;
        public ExaEase ease;
    }
}