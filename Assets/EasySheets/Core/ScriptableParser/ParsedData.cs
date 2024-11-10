using System;
using System.Collections.Generic;
using Google.Apis.Sheets.v4.Data;
using UnityEngine;

namespace EasySheets.Core
{
    public class ParsedDataList
    {
        protected List<ParsedData> _data { get; private set; }
        
        public int count => _data.Count;
        
        public ParsedDataList(List<ValueRange> data)
        {
            _data = data.ConvertAll(d => new ParsedData(d));
        }
        
        public ParsedDataList(List<ParsedData> data)
        {
            _data = data;
        }
        
        public bool TryGetParsedData(int index, out ParsedData parsedData)
        {
            if (index < 0 || index >= _data.Count)
            {
                parsedData = null;
                return false;
            }
            parsedData = _data[index];
            return true;
        }
    }
    
    public class ParsedData 
    {
        public ValueRange _data { get; private set; }
        public int rowCount => GetRowCount();
        public int columnCount => GetColumnCount();
        
        public ParsedData(ValueRange data)
        {
            _data = data;
        }
        
        public bool CheckRange(int row, int column)
        {
            try
            {
                if(_data.Values[row][column] != null)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return false;
        }
        
        public string GetString(int row, int column)
        {
            if (!CheckRange(row, column))
            {
                return "";
            }
            return _data.Values[row][column].ToString();
        }
        
        public int GetInt(int row, int column)
        {
            if (!CheckRange(row, column))
            {
                return 0;
            }
            return int.Parse(_data.Values[row][column].ToString());
        }
        
        public float GetFloat(int row, int column)
        {
            if (!CheckRange(row, column))
            {
                return 0;
            }
            return float.Parse(_data.Values[row][column].ToString(), System.Globalization.CultureInfo.InvariantCulture);
        }
        
        public T GetEnum<T>(int row, int column) where T : struct, System.Enum
        {
            if (!CheckRange(row, column))
            {
                return default(T);
            }
            return EnumParser.ParseEnum<T>(_data.Values[row][column].ToString());
        }
        
        private int GetRowCount()
        {
            return _data.Values.Count;
        }
        
        private int GetColumnCount()
        {
            return _data.Values[0].Count;
        }
        
        public void PrintAllValues()
        {
            foreach (var value in _data.Values)
            {
                foreach (var v in value)
                {
                    Debug.Log(v);
                }
            }
        }
        
        
    }
}