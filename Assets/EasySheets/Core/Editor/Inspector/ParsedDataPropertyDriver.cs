using UnityEngine;
using UnityEditor;
using System.Reflection;
using EasySheets.Core;

[CustomPropertyDrawer(typeof(RangeData))]
public class RangeDataDrawer : PropertyDrawer
{
    private const float verticalSpacing = 2f;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float totalHeight = 0f;
        float lineHeight = EditorGUIUtility.singleLineHeight;
        totalHeight += lineHeight + verticalSpacing;
        totalHeight += lineHeight + verticalSpacing;
        SerializedProperty descriptionProp = property.FindPropertyRelative("_description");
        if (descriptionProp != null)
        {
            float descriptionHeight = EditorGUI.GetPropertyHeight(descriptionProp, true);
            totalHeight += descriptionHeight + verticalSpacing;
        }
        totalHeight += lineHeight + verticalSpacing;

        return totalHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        Rect rect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

        // Поле _pageName
        SerializedProperty pageNameProp = property.FindPropertyRelative("_pageName");
        EditorGUI.PropertyField(rect, pageNameProp, new GUIContent("Page Name"));
        rect.y += EditorGUIUtility.singleLineHeight + verticalSpacing;

        // Поле _range
        SerializedProperty rangeProp = property.FindPropertyRelative("_range");
        EditorGUI.PropertyField(rect, rangeProp, new GUIContent("Range"));
        rect.y += EditorGUIUtility.singleLineHeight + verticalSpacing;

        // Поле _description
        SerializedProperty descriptionProp = property.FindPropertyRelative("_description");
        if (descriptionProp != null)
        {
            float descriptionHeight = EditorGUI.GetPropertyHeight(descriptionProp, true);
            Rect descriptionRect = new Rect(rect.x, rect.y, rect.width, descriptionHeight);
            EditorGUI.PropertyField(descriptionRect, descriptionProp, new GUIContent("Description"), true);
            rect.y += descriptionHeight + verticalSpacing;
        }

        if (GUI.Button(rect, "Open ValueRange"))
        {
            RangeData rangeData = GetTargetObjectOfProperty(property) as RangeData;
            if (rangeData != null)
            {
                ValueRangeWindow.ShowWindow(rangeData.valueRange);
            }
            else
            {
                Debug.LogError("Null RangeData in SerializedProperty.");
            }
        }
        
        EditorGUI.EndProperty();
    }
    
    public static object GetTargetObjectOfProperty(SerializedProperty prop)
    {
        if (prop == null)
            return null;

        string path = prop.propertyPath.Replace(".Array.data[", "[");
        object obj = prop.serializedObject.targetObject;
        string[] elements = path.Split('.');
        foreach (var element in elements)
        {
            if (element.Contains("["))
            {
                string elementName = element.Substring(0, element.IndexOf("["));
                int index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                obj = GetValue(obj, elementName, index);
            }
            else
            {
                obj = GetValue(obj, element);
            }
        }
        return obj;
    }

    public static object GetValue(object source, string name)
    {
        if (source == null)
            return null;
        var type = source.GetType();
        while (type != null)
        {
            var field = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (field != null)
                return field.GetValue(source);
            var property = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (property != null)
                return property.GetValue(source, null);
            type = type.BaseType;
        }
        return null;
    }

    public static object GetValue(object source, string name, int index)
    {
        var enumerable = GetValue(source, name) as System.Collections.IEnumerable;
        if (enumerable == null)
            return null;
        var enm = enumerable.GetEnumerator();
        for (int i = 0; i <= index; i++)
        {
            if (!enm.MoveNext())
                return null;
        }
        return enm.Current;
    }
}