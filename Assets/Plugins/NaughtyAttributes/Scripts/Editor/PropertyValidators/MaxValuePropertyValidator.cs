﻿using UnityEditor;
using UnityEngine;

namespace NaughtyAttributes.Editor {
    public class MaxValuePropertyValidator : PropertyValidatorBase {
        public override void ValidateProperty(SerializedProperty property) {
            var maxValueAttribute = PropertyUtility.GetAttribute<MaxValueAttribute>(property);

            if (property.propertyType == SerializedPropertyType.Integer) {
                if (property.intValue > maxValueAttribute.MaxValue) {
                    property.intValue = (int) maxValueAttribute.MaxValue;
                }
            } else if (property.propertyType == SerializedPropertyType.Float) {
                if (property.floatValue > maxValueAttribute.MaxValue) {
                    property.floatValue = maxValueAttribute.MaxValue;
                }
            } else {
                var warning = maxValueAttribute.GetType().Name + " can be used only on int or float fields";
                Debug.LogWarning(warning, property.serializedObject.targetObject);
            }
        }
    }
}