using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasySheets.Core;
using Google.Apis.Sheets.v4.Data;

public static class EasyParser
{
    private static GoogleSheetsService _googleSheetsService = new GoogleSheetsService(EasySheetsConfig.Instance.CredentialPath, EasySheetsConfig.Instance.ApplicationName);
    
    public static List<T> ParseRowsData<T>(ParsedData data, Func<int, T> parseFunc)
    {
        var parsedDataList = new List<T>();
        
        for (int i = 0; i < data.rowCount; i++)
        {
            if (!data.CheckRange(i, 0)) continue;
            parsedDataList.Add(parseFunc(i));
        }

        return parsedDataList;
    }
    
    public static List<T> ParseColumnsData<T>(ParsedData data, Func<int, T> parseFunc)
    {
        var parsedDataList = new List<T>();
        
        for (int i = 0; i < data.columnCount; i++)
        {
            if (!data.CheckRange(0, i)) continue;
            parsedDataList.Add(parseFunc(i));
        }

        return parsedDataList;
    }

    public static async Task<List<T>> ParseColumnsDataAsync<T>(string sheetsID, string range, Func<int, T> parseFunc)
    {
        var data = await GetSheetDataFromRangeAsync(sheetsID, range);
        return ParseColumnsData(data, parseFunc);  
    }
    
    public static async Task<List<T>> ParseRowsDataAsync<T>(string sheetsID, string range, Func<int, T> parseFunc)
    {
        var data = await GetSheetDataFromRangeAsync(sheetsID, range);
        return ParseRowsData(data, parseFunc);  
    } 
    
    public static async Task<ParsedData> GetSheetDataFromRangeAsync(string sheetsID, string range)
    {
        return new ParsedData(await _googleSheetsService.GetSheetDataAsync(sheetsID, range));
    }
    
    
}




