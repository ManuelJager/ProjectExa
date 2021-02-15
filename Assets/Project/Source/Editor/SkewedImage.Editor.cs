using Exa.UI;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine.UI;

namespace Exa.CustomEditors
{
    [CustomEditor(typeof(PixelSkewedImage), true)]
    public class SkewedImageEditor : ImageEditor
    {
        private SerializedProperty skew;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            skew = serializedObject.FindProperty(nameof(PixelSkewedImage.skew));
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            // ReSharper disable once PossibleNullReferenceException
            (target as Graphic).SetAllDirty();
            (target as AngleSkewedImage)?.RecalculatePixels();
            
            EditorGUILayout.PropertyField(skew);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
