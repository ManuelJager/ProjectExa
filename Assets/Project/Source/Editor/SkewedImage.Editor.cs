using Exa.UI;
using UnityEditor;
using UnityEditor.UI;

namespace Exa.CustomEditors
{
    [CustomEditor(typeof(SkewedImage))]
    public class SkewedImageEditor : ImageEditor
    {
        private SerializedProperty skewX;
        private SerializedProperty skewY;

        protected override void OnEnable()
        {
            base.OnEnable();

            skewX = serializedObject.FindProperty(nameof(SkewedImage.skewX));
            skewY = serializedObject.FindProperty(nameof(SkewedImage.skewY));
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            // ReSharper disable once PossibleNullReferenceException
            (target as SkewedImage).SetAllDirty();

            EditorGUILayout.PropertyField(skewX);
            EditorGUILayout.PropertyField(skewY);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
