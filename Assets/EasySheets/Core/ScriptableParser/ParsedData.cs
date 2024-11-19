using System;
using System.Collections.Generic;
using System.Linq;
using Google.Apis.Sheets.v4.Data;
using UnityEngine;

namespace EasySheets.Core
{
    /// <summary>
    /// Represents parsed data from a Google Sheets value range, providing methods for data extraction and validation.
    /// </summary>
    public class ParsedData 
    {
        /// <summary>
        /// The ValueRange object representing the data from the Google Sheets.
        /// </summary>
        public ValueRange _data { get; private set; }
        
        /// <summary>
        /// Gets the number of rows in the parsed data.
        /// </summary>
        public int rowCount => GetRowCount();
        
        /// <summary>
        /// Gets the number of columns in the parsed data.
        /// </summary>
        public int columnCount => GetColumnCount();
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ParsedData"/> class.
        /// </summary>
        /// <param name="data">The <see cref="ValueRange"/> object containing the data to be parsed.</param>
        public ParsedData(ValueRange data)
        {
            _data = data;
        }
        
        /// <summary>
        /// Checks if a specified cell in the data range is within bounds and not null.
        /// </summary>
        /// <param name="row">The row index of the cell.</param>
        /// <param name="column">The column index of the cell.</param>
        /// <returns>True if the cell is within range and not null; otherwise, false.</returns>
        public bool CheckRange(int row, int column)
        {
            try
            {
                if (_data.Values[row][column] != null)
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
        
        /// <summary>
        /// Gets the string value from a specified cell.
        /// </summary>
        /// <param name="row">The row index of the cell.</param>
        /// <param name="column">The column index of the cell.</param>
        /// <returns>The string value of the cell, or an empty string if the cell is out of range or empty.</returns>
        public string GetString(int row, int column)
        {
            if (!CheckRange(row, column) || IsEmptyCell(row, column))
            {
                return "";
            }
            return _data.Values[row][column].ToString();
        }
        
        /// <summary>
        /// Gets a list of integers from a specified cell, using a specified separator.
        /// </summary>
        /// <param name="row">The row index of the cell.</param>
        /// <param name="column">The column index of the cell.</param>
        /// <param name="separator">The character used to separate values in the cell. Default is '/'.</param>
        /// <returns>A list of integers parsed from the cell.</returns>
        public List<int> GetIntList(int row, int column, char separator = '/')
        {
            if (!CheckRange(row, column) || IsEmptyCell(row, column))
            {
                return new List<int>();
            }
            return ParsingTools.GetIntList(_data.Values[row][column].ToString());
        }
        
        /// <summary>
        /// Gets a list of floats from a specified cell, using a specified separator.
        /// </summary>
        /// <param name="row">The row index of the cell.</param>
        /// <param name="column">The column index of the cell.</param>
        /// <param name="separator">The character used to separate values in the cell. Default is '/'.</param>
        /// <returns>A list of floats parsed from the cell.</returns>
        public List<float> GetFloatList(int row, int column, char separator = '/')
        {
            if (!CheckRange(row, column) || IsEmptyCell(row, column))
            {
                return new List<float>();
            }
            return ParsingTools.GetFloatList(_data.Values[row][column].ToString());
        }
        
        /// <summary>
        /// Gets a list of enum values from a specified cell, using a specified separator.
        /// </summary>
        /// <typeparam name="T">The type of enum to parse. Must be a struct and an enum type.</typeparam>
        /// <param name="row">The row index of the cell.</param>
        /// <param name="column">The column index of the cell.</param>
        /// <param name="separator">The character used to separate values in the cell. Default is ','.</param>
        /// <returns>A list of enum values parsed from the cell.</returns>
        public List<T> GetEnumList<T>(int row, int column, char separator = ',') where T : struct, System.Enum
        {
            if (!CheckRange(row, column) || IsEmptyCell(row, column))
            {
                return new List<T>();
            }
            return ParsingTools.GetEnumList<T>(_data.Values[row][column].ToString());
        }
        
        /// <summary>
        /// Gets a list of strings from a specified cell, using a specified separator.
        /// </summary>
        /// <param name="row">The row index of the cell.</param>
        /// <param name="column">The column index of the cell.</param>
        /// <param name="separator">The character used to separate values in the cell. Default is '/'.</param>
        /// <returns>A list of strings parsed from the cell.</returns>
        public List<string> GetStringList(int row, int column, char separator = '/')
        {
            if (!CheckRange(row, column))
            {
                return new List<string>();
            }
            return ParsingTools.GetStringList(_data.Values[row][column].ToString());
        }
        
        /// <summary>
        /// Gets an integer value from a specified cell.
        /// </summary>
        /// <param name="row">The row index of the cell.</param>
        /// <param name="column">The column index of the cell.</param>
        /// <returns>The integer value of the cell, or 0 if the cell is out of range or empty.</returns>
        public int GetInt(int row, int column)
        {
            if (!CheckRange(row, column) || IsEmptyCell(row, column))
            {
                return 0;
            }
            return ParsingTools.GetInt(_data.Values[row][column].ToString());
        }
        
        /// <summary>
        /// Gets a float value from a specified cell.
        /// </summary>
        /// <param name="row">The row index of the cell.</param>
        /// <param name="column">The column index of the cell.</param>
        /// <returns>The float value of the cell, or 0 if the cell is out of range or empty.</returns>
        public float GetFloat(int row, int column)
        {
            if (!CheckRange(row, column) || IsEmptyCell(row, column))
            {
                return 0;
            }
            return ParsingTools.GetFloat(_data.Values[row][column].ToString());
        }
        
        /// <summary>
        /// Checks if a specified cell is empty.
        /// </summary>
        /// <param name="row">The row index of the cell.</param>
        /// <param name="column">The column index of the cell.</param>
        /// <returns>True if the cell is empty; otherwise, false.</returns>
        private bool IsEmptyCell(int row, int column)
        {
            return _data.Values[row][column].ToString() == "";
        }
        
        /// <summary>
        /// Gets an enum value from a specified cell.
        /// </summary>
        /// <typeparam name="T">The type of enum to parse. Must be a struct and an enum type.</typeparam>
        /// <param name="row">The row index of the cell.</param>
        /// <param name="column">The column index of the cell.</param>
        /// <returns>The enum value of the cell, or the default value if the cell is out of range or empty.</returns>
        public T GetEnum<T>(int row, int column) where T : struct, System.Enum
        {
            if (!CheckRange(row, column) && IsEmptyCell(row, column))
            {
                return default(T);
            }
            return ParsingTools.GetEnum<T>(_data.Values[row][column].ToString());
        }
        
        /// <summary>
        /// Gets the number of rows in the data.
        /// </summary>
        /// <returns>The number of rows in the data.</returns>
        private int GetRowCount()
        {
            return _data.Values.Count;
        }
        
        /// <summary>
        /// Gets the number of columns in the first row of the data.
        /// </summary>
        /// <returns>The number of columns in the first row of the data.</returns>
        private int GetColumnCount()
        {
            return _data.Values[0].Count;
        }
        
        /// <summary>
        /// Prints all values in the parsed data to the Unity console.
        /// </summary>
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

        /// <summary>
        /// Checks if a sub-row at the specified cell is empty.
        /// </summary>
        /// <param name="row">The row index of the cell.</param>
        /// <param name="column">The column index of the cell.</param>
        /// <returns>True if the sub-row at the specified cell is empty; otherwise, false.</returns>
        public bool CheckSubRow(int row, int column)
        {
            try
            {
                if (_data.Values[row][column].ToString() == "")
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
    }
}
