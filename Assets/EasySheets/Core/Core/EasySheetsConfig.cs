using System;
using System.Collections;
using System.Collections.Generic;
using EasySheets.Core;
using UnityEngine;

public class EasySheetsConfig : ScriptableObject
{
    private static EasySheetsConfig _instance;

    public static EasySheetsConfig Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<EasySheetsConfig>("EasySheets/EasySheetConfig");
                if (_instance == null)
                {
                    Debug.LogError("MySingleton asset not found in Resources folder. Please create one.");
                }
            }
            return _instance;
        }
    }
    
    public string ApplicationName = "EasySheets";
    public string CredentialPath;
    public string FileName;
    public string Path => Application.streamingAssetsPath + "/" + FileName;
    
}
