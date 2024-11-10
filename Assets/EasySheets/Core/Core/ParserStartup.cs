using System;
using System.Collections.Generic;
using UnityEngine;

namespace EasySheets.Core.Core
{
    public class ParserStartup : MonoBehaviour
    {
        public event Action OnSheetsParsed;
        [field: SerializeField] public List<ParsedScriptableObject> parsedScriptableObjects { get; private set; }
        private int _populatedCount = 0;
        public float progress => _populatedCount / (float)parsedScriptableObjects.Count;
        public void Awake()
        {
            foreach (var parsedScriptableObject in parsedScriptableObjects)
            {
                parsedScriptableObject.OnDataPopulated += OnPopulateComplete;
                parsedScriptableObject.Populate();
            }
        }

        private void OnDestroy()
        {
            foreach (var parsedScriptableObject in parsedScriptableObjects)
            {
                parsedScriptableObject.OnDataPopulated -= OnPopulateComplete;
            }
        }

        public void OnPopulateComplete(ParsedScriptableObject parsedScriptableObject)
        {
            _populatedCount++;
            Debug.Log($"Data populated for {parsedScriptableObject.name}");
            UpdateProgress();
        }
        
        public void UpdateProgress()
        {
            if( _populatedCount >= parsedScriptableObjects.Count)
            {
                OnSheetsParsed?.Invoke();
                Debug.Log("All data populated");
            }
        }
    }
}
