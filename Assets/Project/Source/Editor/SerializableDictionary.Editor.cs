using Exa.Types.Generics;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace Exa.CustomEditors {
    [CustomPropertyDrawer(typeof(SerializableDictionary), true)]
    public class SerializableDictionaryDrawer : PropertyDrawer {
        private ReorderableList list;
     
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
            if (list == null) {
                var listProp = property.FindPropertyRelative("list");
                list = new ReorderableList(property.serializedObject, listProp, true, false, true, true);
                list.drawElementCallback = DrawListItems;
            }
     
            var firstLine = position;
            firstLine.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(firstLine, property, label);
     
            if (property.isExpanded) {
                position.y += firstLine.height;
     
                if (_elementIndex == null) {
                    _elementIndex = new GUIContent();
                }

                list.DoList(position);
            }
        }
     
        private static GUIContent[] PairElementLabels {
            get => _pairElementLabels ??= new[] {
                new GUIContent("Key"),
                new GUIContent("=>")
            };
        }

        private static GUIContent[] _pairElementLabels;
        private static GUIContent _elementIndex;

        private void DrawListItems(Rect rect, int index, bool isActive, bool isFocused) {
            var element = list.serializedProperty.GetArrayElementAtIndex(index); // The element in the list
     
            var keyProp   = element.FindPropertyRelative("key");
            var valueProp = element.FindPropertyRelative("value");
     
            _elementIndex.text = $"Element {index}";
            /*var label =*/ EditorGUI.BeginProperty(rect, _elementIndex, element);
     
            var prevLabelWidth = EditorGUIUtility.labelWidth;
     
            EditorGUIUtility.labelWidth = 75;
     
            var rect0 = rect; //EditorGUI.PrefixLabel(rect, GUIUtility.GetControlID(FocusType.Passive), label);
     
            var halfWidth = rect0.width / 2f;
            rect0.width = halfWidth;
            rect0.y += 1f;
            rect0.height -= 2f;
     
     
            EditorGUIUtility.labelWidth = 40;
     
            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(rect0, keyProp);
     
            rect0.x += halfWidth + 4f;
     
            EditorGUI.PropertyField(rect0, valueProp);
     
            EditorGUIUtility.labelWidth = prevLabelWidth;
     
            EditorGUI.EndProperty();
        }
     
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            if (property.isExpanded) {
                var listProp = property.FindPropertyRelative("list");

                return listProp.arraySize < 2 
                    ? EditorGUIUtility.singleLineHeight + 52f 
                    : EditorGUIUtility.singleLineHeight + 23f * listProp.arraySize + 29;
            }
            
            return EditorGUIUtility.singleLineHeight;
        }
    }
}