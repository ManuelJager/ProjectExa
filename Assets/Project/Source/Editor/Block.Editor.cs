﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using Exa.Types;
using UnityEditor;

namespace Exa.CustomEditors {
    [CustomEditor(typeof(Block), true)]
    public class BlockEditor : Editor {
        private DefaultDict<Type, BlockDataInfo> typeCache;

        private void OnEnable() {
            typeCache = new DefaultDict<Type, BlockDataInfo>(type => new BlockDataInfo(type));
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            base.OnInspectorGUI();

            foreach (var behaviour in ((Block) target).GetBehaviours()) {
                var data = behaviour.BlockComponentData;
                data = typeCache[data.GetType()].Draw(data);
                behaviour.BlockComponentData = data;
            }
        }

        private class BlockDataInfo {
            private readonly string name;
            private readonly IEnumerable<FieldInfo> properties;

            public BlockDataInfo(Type dataType) {
                name = dataType.Name;
                properties = dataType.GetFields().ToList();
            }

            public IBlockComponentValues Draw(IBlockComponentValues values) {
                EditorGUILayout.Space();
                EditorGUILayout.BeginFoldoutHeaderGroup(true, name);

                foreach (var prop in properties) {
                    var currentValue = prop.GetValue(values);
                    var value = DrawField(prop.Name, currentValue, prop.FieldType);

                    if (value != null && !currentValue.Equals(value)) {
                        prop.SetValue(values, value);
                    }
                }

                EditorGUILayout.EndFoldoutHeaderGroup();

                return values;
            }

            private object DrawField(string name, object c, Type cType) {
                if (cType == typeof(bool)) {
                    return EditorGUILayout.Toggle(name, (bool) c);
                }

                if (cType == typeof(float)) {
                    return EditorGUILayout.FloatField(name, (float) c);
                }

                return null;
            }
        }
    }
}