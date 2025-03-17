using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EasySheets.Core
{
    public static class ParsingTools
    {
        /// <summary>
        /// Parses a string into a list of integers using the specified separator.
        /// </summary>
        /// <param name="s">The input string containing integers separated by a character.</param>
        /// <param name="separator">The character used to separate the integer values in the string. Default is '/'.</param>
        /// <returns>A list of integers parsed from the input string.</returns>
        public static List<int> GetIntList(string s, char separator = '/')
        {
            return new List<int>(s.Split(separator).Select(int.Parse));
        }
        
        /// <summary>
        /// Parses a string into a list of floating-point numbers using the specified separator.
        /// </summary>
        /// <param name="s">The input string containing float values separated by a character.</param>
        /// <param name="separator">The character used to separate the float values in the string. Default is '/'.</param>
        /// <returns>A list of floats parsed from the input string.</returns>
        public static List<float> GetFloatList(string s, char separator = '/')
        {
            return new List<float>(s.Split(separator).Select(GetFloat));
        }
        
        /// <summary>
        /// Parses a string into a list of enum values using the specified separator.
        /// </summary>
        /// <typeparam name="T">The type of the enum to parse. Must be a struct and an enum type.</typeparam>
        /// <param name="s">The input string containing enum values separated by a character.</param>
        /// <param name="separator">The character used to separate the enum values in the string. Default is ','.</param>
        /// <returns>A list of enum values parsed from the input string.</returns>
        public static List<T> GetEnumList<T>(string s, char separator = ',') where T : struct, System.Enum
        {
            return new List<T>(s.Split(separator).Select(EnumParser.ParseEnum<T>));
        }
        
        /// <summary>
        /// Parses a string into a list of strings using the specified separator.
        /// </summary>
        /// <param name="s">The input string containing values separated by a character.</param>
        /// <param name="separator">The character used to separate the string values. Default is '/'.</param>
        /// <returns>A list of strings parsed from the input string.</returns>
        public static List<string> GetStringList(string s, char separator = '/')
        {
            return new List<string>(s.ToString().Split(separator));
        }
        
        /// <summary>
        /// Parses a string into an integer.
        /// </summary>
        /// <param name="s">The input string representing an integer.</param>
        /// <returns>The parsed integer value.</returns>
        public static int GetInt(string s)
        {
            return int.Parse(s);
        }
        
        /// <summary>
        /// Parses a string into a floating-point number with culture-invariant formatting.
        /// </summary>
        /// <param name="s">The input string representing a float.</param>
        /// <returns>The parsed float value.</returns>
        public static float GetFloat(string s)
        {
            return float.Parse(s, System.Globalization.CultureInfo.InvariantCulture);
        }
        
        /// <summary>
        /// Parses a string into an enum value of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the enum to parse. Must be a struct and an enum type.</typeparam>
        /// <param name="s">The input string representing an enum value.</param>
        /// <returns>The parsed enum value of type T.</returns>
        public static T GetEnum<T>(string s) where T : struct, System.Enum
        {
            return EnumParser.ParseEnum<T>(s);
        }

        public static Vector3 GetVector3(string squadStateParameter)
        {
            var values = squadStateParameter.Split('/');
            if (values.Length == 3)
            {
                return new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
            }
            else
            {
                return Vector3.zero;
            }
        }
    }
}
