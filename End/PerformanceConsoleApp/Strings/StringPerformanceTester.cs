using PerformanceConsoleApp._Shared;
using System;
using System.Linq;

namespace PerformanceConsoleApp
{
    public static class StringPerformanceTester
    {
        private const int _count = 100000;
        public static void LoopOverCharacters()
        {
            var input = " " + string.Concat(DataGenerator.GetRandomStringArray(50));

            // Check characters in string with ToString() extension method.
            Benchmarker.Benchmark(() =>
            {
                int spaces = 0;
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i].ToString() == " ")
                    {
                        spaces++;
                    }
                }
                Console.WriteLine($"Results: {spaces}");
            }, "Spaces with char ToString()", 1);

            // Check characters in string with Contains() extension method.
            Benchmarker.Benchmark(() =>
            {
                int spaces = input.Count(x => x.Equals(' '));
                Console.WriteLine($"Results: {spaces}");
            }, "Space chars with Linq's Count() method", 1);

            Benchmarker.Benchmark(() =>
            {
                int spaces = input.Count(x => x.Equals(" "));
                Console.WriteLine($"Results: {spaces}");
            }, "Space strings with Linq's Count() method (incorrect result!)", 1);

            // Check characters in string.
            Benchmarker.Benchmark(() =>
            {
                int spaces = 0;
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i] == ' ')
                    {
                        spaces++;
                    }
                }
                Console.WriteLine($"Results: {spaces}");
            }, "Spaces with char", 1);

            Console.WriteLine();

            //Don't use ToString() extension method when checking characters. Don't use LINQ either
        }


        public static void DemoLookupTableToLower()
        {
            string _lookupStringL = "--------------------------------------&-()*+,-./----------:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-";
            char y = 'Y';
            char result;

            Benchmarker.Benchmark(() =>
            {
                result = char.ToLower(y);
            }, "Char.ToLower()", _count);

            Benchmarker.Benchmark(() =>
            {
                result = _lookupStringL[y];
            }, "Char to lower with lookup table", _count);

            Console.WriteLine();

            //A lookup table is a lot faster than the regular ToLower(), but using this alternative could cause localization issues
        }

    }
}
