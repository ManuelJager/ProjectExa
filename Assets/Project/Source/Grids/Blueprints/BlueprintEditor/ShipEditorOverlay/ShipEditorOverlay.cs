using Exa.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Exa.Grids.Blueprints.BlueprintEditor
{
    public class ShipEditorOverlay : MonoBehaviour
    {
        [SerializeField] private Hoverable blueprintInventoryHoverable;
        [SerializeField] private Hoverable blueprintInfoPanelHoverable;
        [SerializeField] private Hoverable editorStatePanelHoverable;

        public CanvasGroupInteractibleAdapter editorOverlayCanvasGroup;
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
            blueprintInventoryHoverable.onPointerEnter.AddListener(action);
            blueprintInfoPanelHoverable.onPointerEnter.AddListener(action);
            editorStatePanelHoverable.onPointerEnter.AddListener(action);
        }

        private void AddListenerToGroupOnPointerExit(UnityAction action)
        {
            blueprintInventoryHoverable.onPointerExit.AddListener(action);
            blueprintInfoPanelHoverable.onPointerExit.AddListener(action);
            editorStatePanelHoverable.onPointerExit.AddListener(action);
        }
    }
}