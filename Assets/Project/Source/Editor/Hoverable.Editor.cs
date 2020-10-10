using Exa.UI;
using UnityEditor;

namespace Exa.CustomEditors
{
    [CustomEditor(typeof(Hoverable))]
    [CanEditMultipleObjects]
    public class HoverableEditor : Editor
    {
        private SerializedProperty onPointerEnter;
        private SerializedProperty onPointerExit;
        private SerializedProperty invokeStateChangeOnHover;
        private SerializedProperty cursorState;
        private SerializedProperty checkMouseInsideRectOnEnable;

        protected void OnEnable()
        {
            onPointerEnter = serializedObject.FindProperty("onPointerEnter");
            onPointerExit = serializedObject.FindProperty("onPointerExit");
            invokeStateChangeOnHover = serializedObject.FindProperty("invokeStateChangeOnHover");
            cursorState = serializedObject.FindProperty("cursorOverride");
            checkMouseInsideRectOnEnable = serializedObject.FindProperty("checkMouseInsideRectOnEnable");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(checkMouseInsideRectOnEnable);
            EditorGUILayout.PropertyField(invokeStateChangeOnHover);

            if (invokeStateChangeOnHover.boolValue)
            {
                EditorGUI.indentLevel = 1;
                EditorGUILayout.PropertyField(cursorState);
                EditorGUI.indentLevel = 0;
            }

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(onPointerEnter);
            EditorGUILayout.PropertyField(onPointerExit);

            serializedObject.ApplyModifiedProperties();
        }
    }
}