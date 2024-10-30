using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasySheets.Core;
using UnityEngine;

public class AutoParser : MonoBehaviour
{
    [SerializeField] private List<ParsedScriptableObject> _autoParsedObjects;
    
    private void Start()
    {
        Debug.Log("AutoParser started");
        foreach (var parsedScriptableObject in _autoParsedObjects)
        {
            parsedScriptableObject.Populate();
        }
        
    }
}
