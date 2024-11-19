using System;
using System.Linq;
using UnityEngine;

namespace EasySheets.Core
{
    public static class EnumParser
    {
        public static T ParseEnum<T>(string enumString) where T : struct, Enum
        {
            Debug.Log(enumString);
            enumString = enumString.Replace(" ", "");
            if (Enum.TryParse<T>(enumString, true, out T result))
            {
                return result;
            }
            else
            {
                var enumNames = Enum.GetNames(typeof(T));
                var similarNames = enumNames
                    .Where(name => name.StartsWith(enumString, StringComparison.InvariantCultureIgnoreCase)
                                   || name.IndexOf(enumString, StringComparison.InvariantCultureIgnoreCase) >= 0);

                if (similarNames.Any())
                {
                    throw new ArgumentException($"Некорректное значение для перечисления {typeof(T).Name}: '{enumString}'. Возможно, вы имели в виду: {string.Join(", ", similarNames)}?");
                }
                else
                {
                    throw new ArgumentException($"Некорректное значение для перечисления {typeof(T).Name}: '{enumString}'");
                }
            }
        }


        public static void PrintAllValues<T>() where T : Enum
        {
            foreach (T value in Enum.GetValues(typeof(T)))
            {
                Console.WriteLine(value);
            }
        }
    }
}