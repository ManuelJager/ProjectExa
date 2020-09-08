using Exa.UI;
using UnityEditor;

namespace Exa.CustomEditors
{
    [CustomEditor(typeof(CanvasGroupInteractableAdapter))]
    internal class CanvasGroupInteractibleAdapterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ((CanvasGroupInteractableAdapter)target).Interactable = ((CanvasGroupInteractableAdapter)target).Interactable;
        }
    }
}