using Exa.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Exa.ShipEditor
{
    public class ShipEditorOverlay : MonoBehaviour
    {
        [SerializeField] private Hoverable _blueprintInventoryHoverable;
        [SerializeField] private Hoverable _blueprintInfoPanelHoverable;
        [SerializeField] private Hoverable _editorStatePanelHoverable;

        public CanvasGroupInteractableAdapter editorOverlayCanvasGroup;
        public ShipEditorOverlayInfoPanel blueprintInfoPanel;
        public ShipEditorOverlayInventory inventory;
        public MirrorView mirrorView;

        public UnityEvent onPointerEnter;
        public UnityEvent onPointerExit;

        private void Awake()
        {
            AddListenerToGroupOnPointerEnter(onPointerEnter.Invoke);
            AddListenerToGroupOnPointerExit(onPointerExit.Invoke);
        }

        private void AddListenerToGroupOnPointerEnter(UnityAction action)
        {
            _blueprintInventoryHoverable.onPointerEnter.AddListener(action);
            _blueprintInfoPanelHoverable.onPointerEnter.AddListener(action);
            _editorStatePanelHoverable.onPointerEnter.AddListener(action);
        }

        private void AddListenerToGroupOnPointerExit(UnityAction action)
        {
            _blueprintInventoryHoverable.onPointerExit.AddListener(action);
            _blueprintInfoPanelHoverable.onPointerExit.AddListener(action);
            _editorStatePanelHoverable.onPointerExit.AddListener(action);
        }
    }
}