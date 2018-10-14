using System;
using System.Collections.Generic;
using System.IO;

namespace PerformanceConsoleApp._Shared
{
    public static class DataGenerator
    {
        public static string GetRandomString()
        {
            return Path.GetRandomFileName();
        }

        public static string[] GetRandomStringArray(int length)
        {
            var arr = new string[length];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = GetRandomString();
            }
            return arr;
        }

        public static int GetRandomInt(int min = 1, int max = 100)
        {
            var rnd = new Random();
            return rnd.Next(min, max);
        }

        public static int[] GetRandomIntArray(int length, int min = 1, int max = 100)
        {
            var arr = new int[length];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = GetRandomInt(min, max);
            }
            return arr;
        }

        public static Dictionary<int, int> GetRandomIntDictionary(int length, int min = 1, int max = 100)
        {
            var dictionary = new Dictionary<int, int>();
            for (int i = 0; i < length; i++)
            {
                var @int = GetRandomInt(min, max);
                while (dictionary.TryGetValue(@int, out var value))
                {
                    @int = GetRandomInt(min, max);
                }
                dictionary.Add(@int, @int);
            }
            return dictionary;
        }

        public static int[][] GetJaggedArray(int height, int width)
        {
            var array = new int[width][];
            for (int i = 0; i < width; i++)
            {
                array[i] = new int[height];
            }
            return array;
        }

        public static int[,] Get2DArray(int height, int width)
        {
            var array = new int[height, width];
            return array;
        }

        public static void GetMultipleStrings(out string string1, out string string2)
        {
            string1 = GetRandomString();
            string2 = GetRandomString();
        }

        public static KeyValuePair<string, string> GetMultipleStringsAsKeyValuePair()
        {
            return new KeyValuePair<string, string>(GetRandomString(),GetRandomString());
        }

        public static (string string1, string string2) GetMultipleStringsAsValueTuple()
        {
            return (GetRandomString(), GetRandomString());
        }
    }
}
