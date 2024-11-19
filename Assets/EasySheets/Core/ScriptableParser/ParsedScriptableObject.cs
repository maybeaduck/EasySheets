using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Google.Apis.Sheets.v4.Data;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace EasySheets.Core
{
    public abstract class ParsedScriptableObject : ScriptableObject
    {
        [SerializeField] private string _sheetsID;
        [SerializeField] private List<RangeData> _ranges;
        
        public event Action<ParsedScriptableObject> OnDataPopulated;
        
        [ContextMenu("Populate")]
        public virtual async void Populate()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var data = await ParseData();
            
            PopulateData(new ParsedDataList(data));
            stopwatch.Stop();
            OnDataPopulated?.Invoke(this);
            Debug.Log($"Data populated in {stopwatch.ElapsedMilliseconds} ms");
        }
        
        protected abstract void PopulateData(ParsedDataList data);
        
        protected virtual async Task<List<ParsedData>> ParseData()
        {   
            var values = new List<ParsedData>();
            foreach (var range in _ranges)
            {
                var data = await EasyParser.GetSheetDataFromRangeAsync(_sheetsID, range.range);
                values.Add(data);
                range.valueRange = data._data;
            }
            
            return values;
        }
        
        
    }
}