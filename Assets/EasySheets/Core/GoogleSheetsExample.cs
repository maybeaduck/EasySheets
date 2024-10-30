using UnityEngine;

namespace EasySheets.Core
{
    public class GoogleSheetsExample : MonoBehaviour
    {
        [SerializeField] private string _sheetsID;
        [SerializeField] private string _range = "Лист1!A1:A10";
        private GoogleSheetsService _googleSheetsService;
        
        private async void Start()
        {
            string credentialsPath = Application.streamingAssetsPath + "/credential.json";
            string applicationName = "EasySheets";
        
            // Инициализация сервиса
            _googleSheetsService = new GoogleSheetsService(credentialsPath, applicationName);

            // Параметры таблицы
            string spreadsheetId = _sheetsID;
            string range = _range; // Диапазон данных для получения

            // Получение данных
            var data = await _googleSheetsService.GetSheetDataAsync(spreadsheetId, range);

            if (data != null)
            {
                foreach (var row in data.Values)
                {
                    Debug.Log(string.Join(", ", row));
                }
            }
        }
    }
}