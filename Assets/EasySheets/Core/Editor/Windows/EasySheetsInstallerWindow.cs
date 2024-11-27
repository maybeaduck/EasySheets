using UnityEditor;
using UnityEngine;
using System.Diagnostics;

namespace EasySheets.Core.Editor.Windows
{
    public class EasySheetsInstallerWindow : EditorWindow
    {
        private Vector2 scrollPosition;

        [MenuItem("Tools/EasySheets/Installer Guide", priority = 0)]
        public static void ShowWindow()
        {
            EasySheetsInstallerWindow window = GetWindow<EasySheetsInstallerWindow>("EasySheets Installer");
            window.minSize = new Vector2(500, 600);
        }

        [InitializeOnLoadMethod]
        private static void ShowOnInstall()
        {
            if (!EditorPrefs.HasKey("EasySheets_Installed"))
            {
                ShowWindow();
                EditorPrefs.SetBool("EasySheets_Installed", true);
            }
        }

        private void OnGUI()
        {
            GUILayout.Label("EasySheets Installer Guide", EditorStyles.boldLabel);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Welcome to EasySheets!", EditorStyles.largeLabel);
            EditorGUILayout.HelpBox("Follow these steps to correctly set up the EasySheets plugin in your Unity project.", MessageType.Info);

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            EditorGUILayout.Space();
            DrawHeader("Installation Guide");
            EditorGUILayout.LabelField("Step by step guide to setup EasySheets", EditorStyles.wordWrappedLabel);
            if (GUILayout.Button("Open Guide"))
            {
                OpenUrl("https://jagged-pot-6d5.notion.site/Instalation-Guide-14b203b1ce2380fc9831f8a81ca64034?pvs=4");
            }
            
            DrawHeader("Documentation");
            EditorGUILayout.LabelField("Read the documentation for detailed usage instructions.", EditorStyles.wordWrappedLabel);
            if (GUILayout.Button("Open Documentation"))
            {
                OpenUrl("https://jagged-pot-6d5.notion.site/Easy-Sheets-143203b1ce238046adcbd37d06de50b4?pvs=4");
            }

            EditorGUILayout.Space();
            DrawHeader("Samples");
            EditorGUILayout.LabelField("Access the sample files to explore examples of how to use EasySheets.", EditorStyles.wordWrappedLabel);
            if (GUILayout.Button("Open Samples Folder"))
            {
                EditorUtility.RevealInFinder("Assets/EasySheets/Samples");
            }

            EditorGUILayout.EndScrollView();

            EditorGUILayout.Space(20);
            if (GUILayout.Button("Close", GUILayout.Height(30)))
            {
                this.Close();
            }
        }

        private void DrawHeader(string header)
        {
            EditorGUILayout.LabelField(header, EditorStyles.boldLabel);
            EditorGUILayout.Space(5);
        }

        private void OpenUrl(string url)
        {
            Application.OpenURL(url);
        }

        private void OpenFolder(string folderPath)
        {
            folderPath = System.IO.Path.GetFullPath(folderPath);
            if (System.IO.Directory.Exists(folderPath))
            {
                Process.Start("explorer.exe", folderPath.Replace("/", "\\"));
            }
            else
            {
                UnityEngine.Debug.LogError($"Folder not found: {folderPath}");
            }
        }
    }
}
