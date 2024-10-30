using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Sheets.v4.Data;
using UnityEngine;

namespace EasySheets.Core
{
    public abstract class ParsedScriptableObject : ScriptableObject
    {
        [SerializeField] private string _sheetsID;
        [SerializeField] private List<string> _ranges;

        [ContextMenu("Populate")]
        public virtual async void Populate()
        {
            var data = await ParseData();
            PopulateData(data);
        }
        
        protected abstract void PopulateData(List<ValueRange> data);
        
        protected virtual async Task<List<ValueRange>> ParseData()
        {   
            var values = new List<ValueRange>();
            foreach (var range in _ranges)
            {
                var data = await GetSheetData(_sheetsID, range);
                values.Add(data);
            }

            return values;
        }

        private async Task<ValueRange> GetSheetData(string sheetsID, string range)
        {
            var googleSheetsService = new GoogleSheetsService(EasyParserConfig.CredentialPath, EasyParserConfig.ApplicationName);
            return await googleSheetsService.GetSheetDataAsync(sheetsID, range);
        }
    }
}