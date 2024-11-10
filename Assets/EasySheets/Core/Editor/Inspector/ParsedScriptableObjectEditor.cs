using UnityEditor;
using UnityEngine;

namespace EasySheets.Core.Editor.Inspector
{
    [CustomEditor(typeof(ParsedScriptableObject), true)]
    public class ParsedScriptableObjectEditor : UnityEditor.Editor
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
