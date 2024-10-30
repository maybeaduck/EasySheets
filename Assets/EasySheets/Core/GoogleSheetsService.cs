using System;
using System.IO;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using UnityEngine;

namespace EasySheets.Core
{
    public class GoogleSheetsService
    {
        private SheetsService _sheetsService;

        public GoogleSheetsService(string credentialsPath, string applicationName)
        {
            GoogleCredential credential;
            
            using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(SheetsService.Scope.SpreadsheetsReadonly);
            }
            
            _sheetsService = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName
            });
        }
        
        public async Task<ValueRange> GetSheetDataAsync(string spreadsheetId, string range)
        {
            try
            {
                var request = _sheetsService.Spreadsheets.Values.Get(spreadsheetId, range);
                var response = await request.ExecuteAsync();

                return response;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Ошибка при получении данных из Google Sheets: {ex.Message}");
                return null;
            }
        }
    }
}