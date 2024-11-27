using UnityEditor;
using UnityEngine;
using System.IO;

namespace EasySheets.Core.Editor.Windows
{
    public class EasySheetsMenuWindow : EditorWindow
    {
        private EasySheetsConfig _config;

        [MenuItem("Tools/EasySheets/Config")]
        public static void ShowWindow()
        {
            GetWindow<EasySheetsMenuWindow>("Easy Sheets Config");
        }

        private void OnEnable()
        {
            LoadOrCreateConfig();
        }

        private void OnGUI()
        {
            if (_config == null)
            {
                EditorGUILayout.LabelField("Config not found. Creating a new one...");
                if (GUILayout.Button("Create Config"))
                {
                    CreateConfig();
                }
            }
            else
            {
                EditorGUILayout.LabelField("Configure Easy Sheets Settings", EditorStyles.boldLabel);

                EditorGUI.BeginChangeCheck();

                // Поле для названия приложения
                _config.ApplicationName = EditorGUILayout.TextField("Application Name", _config.ApplicationName);

                // Поле для выбора пути к учетным данным
                EditorGUILayout.BeginHorizontal();
                _config.CredentialPath = EditorGUILayout.TextField("Credential Path", _config.CredentialPath);
                if (GUILayout.Button("Select file", GUILayout.Width(100)))
                {
                    string path = EditorUtility.OpenFilePanel("Select Credential File", Application.streamingAssetsPath, "json");
                    if (!string.IsNullOrEmpty(path))
                    {
                        _config.CredentialPath = path;
                    }
                }
                EditorGUILayout.EndHorizontal();

                // Кнопка для открытия папки учетных данных
                if (GUILayout.Button("Open Credential Folder"))
                {
                    OpenOrCreateCredentialFolder();
                }

                // Если данные изменены, сохраняем автоматически
                if (EditorGUI.EndChangeCheck())
                {
                    SaveConfig();
                }

                // Кнопка для ручного сохранения
                if (GUILayout.Button("Save"))
                {
                    SaveConfig();
                }
            }
        }

        private void LoadOrCreateConfig()
        {
            string[] assets = AssetDatabase.FindAssets("t:EasySheetsConfig");
            if (assets.Length > 0)
            {
                string path = AssetDatabase.GUIDToAssetPath(assets[0]);
                _config = AssetDatabase.LoadAssetAtPath<EasySheetsConfig>(path);
            }
            else
            {
                CreateConfig();
            }
        }

        private void CreateConfig()
        {
            string directoryPath = "Assets/Resources/EasySheets";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string assetPath = $"{directoryPath}/EasySheetConfig.asset";
            _config = ScriptableObject.CreateInstance<EasySheetsConfig>();
            AssetDatabase.CreateAsset(_config, assetPath);
            AssetDatabase.SaveAssets();

            Debug.Log("EasyParserConfig asset created at " + assetPath);
        }

        private void SaveConfig()
        {
            if (_config != null)
            {
                EditorUtility.SetDirty(_config); // Помечаем объект как измененный
                AssetDatabase.SaveAssets();     // Сохраняем изменения в базу данных
                AssetDatabase.Refresh();       // Обновляем состояние базы данных
                Debug.Log("Config saved.");
            }
        }

        private void OpenOrCreateCredentialFolder()
        {
            string folderPath = Application.streamingAssetsPath;
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Debug.Log("Credential folder created at " + folderPath);
            }
            EditorUtility.RevealInFinder(folderPath);
        }
    }
}
