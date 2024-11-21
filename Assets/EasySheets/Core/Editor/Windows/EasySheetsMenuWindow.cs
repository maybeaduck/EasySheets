using UnityEditor;
using UnityEngine;
using System.IO;

namespace EasySheets.Core.Editor.Windows
{
    public class EasySheetsMenuWindow : EditorWindow
    {
        private EasySheetsConfig _config;

        [MenuItem("Tools/Easy Sheets Config")]
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
                    EditorUtility.SetDirty(_config);
                }
            }
            else
            {
                EditorGUILayout.LabelField("Configure Easy Sheets Settings", EditorStyles.boldLabel);

                _config.ApplicationName = EditorGUILayout.TextField("Application Name", _config.ApplicationName);

                EditorGUILayout.BeginHorizontal();
                _config.CredentialPath = EditorGUILayout.TextField("Credential Path", _config.CredentialPath);
                if (GUILayout.Button("Выбрать файл", GUILayout.Width(100)))
                {
                    string path = EditorUtility.OpenFilePanel("Select Credential File", "", "");
                    if (!string.IsNullOrEmpty(path))
                    {
                        _config.CredentialPath = path;
                        EditorUtility.SetDirty(_config);
                    }
                }
                EditorGUILayout.EndHorizontal();

                if (GUI.changed)
                {
                    EditorUtility.SetDirty(_config);
                }
            }
        }

        private void LoadOrCreateConfig()
        {
            string[] assets = AssetDatabase.FindAssets("t:EasyParserConfig");
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
    }
}
