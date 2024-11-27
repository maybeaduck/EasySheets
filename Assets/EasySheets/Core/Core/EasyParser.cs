using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasySheets.Core;
using EasySheets.Example.Medium;
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
    
    public static void ParseSubRowsData<T>(ParsedData data, List<T> parsedDataList, Func<int,bool,int, T> parseFunc,bool isScriptableObject = false)
    {
        bool subRow = false;
        int subIndex = 0;
        int totalSubIndex = 0;
        T parsedValue = default;
        for (int i = 0; i < data.rowCount; i++)
        {
            if (!data.CheckRange(i, 0)) continue;
            if (data.CheckSubRow(i, 0))
            {
                subRow = true;
                parsedValue = parseFunc(i,subRow,i-totalSubIndex-1);
                subIndex++;
                totalSubIndex++;
                if(isScriptableObject) continue;
            
                if (i-totalSubIndex < parsedDataList.Count)
                {
                    // Заменяем существующее значение
                    parsedDataList[i-totalSubIndex] = parsedValue;
                }
                else
                {
                    // Добавляем новое значение
                    parsedDataList.Add(parsedValue);
                }
            }
            else
            {
                subRow = false;
                subIndex = 0;
                parsedValue = parseFunc(i,subRow,i-totalSubIndex-1);
                if( parsedValue == null) continue;
                if(isScriptableObject) continue;
            
                if (i-totalSubIndex < parsedDataList.Count)
                {
                    // Заменяем существующее значение
                    parsedDataList[i-totalSubIndex] = parsedValue;
                }
                else
                {
                    // Добавляем новое значение
                    parsedDataList.Add(parsedValue);
                }
            }
            
        }
    }
    
    public static void ParseRowsData<T>(ParsedData data, List<T> parsedDataList, Func<int, T> parseFunc,bool isScriptableObject = false)
    {
        for (int i = 0; i < data.rowCount; i++)
        {
            if (!data.CheckRange(i, 0)) continue;

            var parsedValue = parseFunc(i);
            if( parsedValue == null) continue;
            if(isScriptableObject) continue;
            
            if (i < parsedDataList.Count)
            {
                parsedDataList[i] = parsedValue;
            }
            else
            {
                parsedDataList.Add(parsedValue);
            }
        }
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




