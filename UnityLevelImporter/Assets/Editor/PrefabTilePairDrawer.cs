using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace UnityLevelImporter
{
	[CustomPropertyDrawer(typeof(PrefabTilePair))]
	public class PrefabTilePairDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var serializedObject = property.serializedObject;
			var targetObject = serializedObject.targetObject;

			var target = (PrefabTilePair)EditorHelpers.GetFieldValueFromPath(targetObject, property.propertyPath) ??
				new PrefabTilePair();

			EditorHelpers.DrawFieldsOnOneLine(
				position, 0,
				x => target.Type = EditorGUI.TextField(x, target.Type),
				x => target.Prefab = (GameObject)EditorGUI.ObjectField(x, target.Prefab, typeof(GameObject), true)
				);
		}
	}
}
