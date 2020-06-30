using Exa.UI;
using UnityEditor;

namespace Exa.CustomEditors
{
    [CustomEditor(typeof(CanvasGroupInteractibleAdapter))]
    internal class CanvasGroupInteractibleAdapterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ((CanvasGroupInteractibleAdapter)target).Interactable = ((CanvasGroupInteractibleAdapter)target).Interactable;
        }
    }
}