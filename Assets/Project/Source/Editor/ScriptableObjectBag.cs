using Exa.Generics;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Exa.CustomEditors
{
    [CustomEditor(typeof(ScriptableObjectBagBase), true)]
    public class ScriptableObjectBagEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Find objects"))
            {
                (target as ScriptableObjectBagBase).FindObjects();
            }
        }
    }
}

