using Exa.UI;
using UnityEditor;

namespace Assets.Project.Source.CustomEditors
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