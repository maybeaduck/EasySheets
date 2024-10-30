using UnityEditor;
using UnityEngine;

namespace EasySheets.Core
{
    [CustomEditor(typeof(ParsedScriptableObject), true)]
    public class ParsedScriptableObjectEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // Рисуем стандартный интерфейс инспектора
            base.OnInspectorGUI();

            // Получаем ссылку на объект
            var parsedObject = (ParsedScriptableObject)target;

            // Добавляем кнопку для вызова метода Populate
            if (GUILayout.Button("Populate"))
            {
                // Вызываем метод Populate
                parsedObject.Populate();
            }
        }
    }
}