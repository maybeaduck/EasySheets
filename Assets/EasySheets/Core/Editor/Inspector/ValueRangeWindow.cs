using System.Collections.Generic;
using Google.Apis.Sheets.v4.Data;
using UnityEditor;
using UnityEngine;

public class ValueRangeWindow : EditorWindow
{
    private ValueRange valueRange;
    private Vector2 scrollPosition;

    public static void ShowWindow(ValueRange valueRange)
    {
        ValueRangeWindow window = GetWindow<ValueRangeWindow>("ValueRange");
        window.valueRange = valueRange;
        window.Show();
    }

    private void OnGUI()
    {
        if (valueRange == null)
        {
            EditorGUILayout.LabelField("ValueRange not found.");
            return;
        }

        // Отображение данных из valueRange
        IList<IList<object>> values = valueRange.Values;
        if (values == null || values.Count == 0)
        {
            EditorGUILayout.LabelField("ValueRange do not contains values.");
            return;
        }

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        foreach (var row in values)
        {
            EditorGUILayout.BeginHorizontal();
            foreach (var cell in row)
            {
                string cellText = cell != null ? cell.ToString() : "null";
                EditorGUILayout.LabelField(cellText, GUILayout.MaxWidth(100));
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();
    }
}