using System;

namespace EasySheets.Core
{
    public static class EnumParser
    {
        public static T ParseEnum<T>(string enumString) where T : struct, Enum
        {
            if (Enum.TryParse<T>(enumString, true, out T result))
            {
                return result;
            }
            else
            {
                throw new ArgumentException($"Invalid value for enum {typeof(T).Name}: {enumString}");
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