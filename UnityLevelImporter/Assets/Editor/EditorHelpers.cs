using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;

namespace UnityLevelImporter
{
	static class EditorHelpers
	{
		internal static void DrawFieldsOnOneLine(
			Rect position,
			float spaceBetweenWidgets,
			params Action<Rect>[] drawGuiWidget)
		{
			float totalSeparationSpace = drawGuiWidget.Length * spaceBetweenWidgets;
			if (totalSeparationSpace > position.width)
				throw new ArgumentException(
					"Too much space between widgets (more space than there is available).",
					"spaceBetweenWidgets");

			float totalWidgetSpace = (position.width - totalSeparationSpace) / drawGuiWidget.Length;
			Vector2 widgetSize = new Vector2(totalWidgetSpace, position.height);

			Vector2 currentWidgetTopLeftCorner = position.min;
			Vector2 separation = Vector2.right * spaceBetweenWidgets;

			foreach (var action in drawGuiWidget)
			{
				Rect widgetBounds = new Rect(currentWidgetTopLeftCorner, widgetSize);
				action(widgetBounds);

				currentWidgetTopLeftCorner += Vector2.Scale(widgetSize, Vector2.right) + separation;
			}
		}

		internal static object GetFieldValueFromPath(object root, string propertyPath)
		{
			Match match = firstPropertyPathSection.Match(propertyPath);
			object fieldValue = root.GetType().GetField(match.Groups["field"].Value);
			string trimmedPath = firstPropertyPathSection.Replace(propertyPath, "");
			if (trimmedPath == string.Empty || fieldValue == null)
				return fieldValue;

			return GetFieldValueFromPath(fieldValue, trimmedPath);
		}

		private static readonly Regex firstPropertyPathSection = new Regex(@"(?<field>^[^\.]+?)\.");
		private static readonly Regex lastPropertyPathSection =
			new Regex(@"(^|\.)(?<field>[^\.]+$)", RegexOptions.ExplicitCapture);
	}
}
