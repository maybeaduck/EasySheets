using System.Collections.Generic;
using Google.Apis.Sheets.v4.Data;
using UnityEngine;

namespace EasySheets.Core
{
    public class ParsedData 
    {
        protected List<ValueRange> _data { get; private set; }
        public int rowCount => GetRowCount();
        public int columnCount => GetColumnCount();

        public ParsedData(List<ValueRange> data)
        {
            _data = data;
        }
        
        public string GetString(int row, int column, int rangeIndex = 0)
        {
            return _data[rangeIndex].Values[row][column].ToString();
        }
        
        public int GetInt(int row, int column, int rangeIndex = 0)
        {
            return int.Parse(_data[rangeIndex].Values[row][column].ToString());
        }
        
        public float GetFloat(int row, int column, int rangeIndex = 0)
        {
            return float.Parse(_data[rangeIndex].Values[row][column].ToString(), System.Globalization.CultureInfo.InvariantCulture);
        }
        
        public T GetEnum<T>(int row, int column, int rangeIndex = 0) where T : struct, System.Enum
        {
            return EnumParser.ParseEnum<T>(_data[rangeIndex].Values[row][column].ToString());
        }
        
        private int GetRowCount(int rangeIndex = 0)
        {
            return _data[rangeIndex].Values.Count;
        }
        
        private int GetColumnCount(int rangeIndex = 0)
        {
            return _data[rangeIndex].Values[0].Count;
        }
        
        public void PrintAllValues(int rangeIndex = 0)
        {
            foreach (var value in _data[rangeIndex].Values)
            {
                foreach (var v in value)
                {
                    Debug.Log(v);
                }
            }
        }
        
        
    }
}