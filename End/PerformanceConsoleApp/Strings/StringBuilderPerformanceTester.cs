using PerformanceConsoleApp._Shared;
using System;
using System.Diagnostics;
using System.Text;

namespace PerformanceConsoleApp
{
    public static class StringBuilderPerformanceTester
    {
        /*
         * When concatinating a number of strings, 
         * StringBuilder is more performant than string concatenation alternatives.
         */
        private const int _count = 100000;
        private static readonly StringBuilder _builder = new StringBuilder();

        public static void DetermineStringBuilderPerformanceVsConcat(string s, int cnt)
        {
            var capacity = cnt * s.Length;

            Console.WriteLine("String count: " + cnt);

            Benchmarker.Benchmark(() =>
            {
                var str = string.Empty;
                for (int i = 1; i < cnt; i++)
                {
                    str += s;
                }
            }, "String Concatenation", 1, cnt);

            Benchmarker.Benchmark(() =>
            {
                var builder = new StringBuilder();
                for (int i = 1; i < cnt; i++)
                {
                    builder.Append(s);
                }
                var str = builder.ToString();
            }, "Default StringBuilder", 1, cnt);

            Benchmarker.Benchmark(() =>
            {
                //TODO 1.1: add stringbuilder capacity
                var builder = new StringBuilder();
                for (int i = 1; i < cnt; i++)
                {
                    builder.Append(s);
                }
                var str = builder.ToString();
            }, "StringBuilder with fixed capacity", 1, cnt);

            Console.WriteLine();
        }

        public static void ExecuteSubstringVsAppendOverload(string s)
        {
            var builder = new StringBuilder();

            Benchmarker.Benchmark(() =>
            {
                builder.Clear();
                string temp = s.Substring(5, 5);
                builder.Append(temp);
            }, "Substring append", _count);

            Benchmarker.Benchmark(() =>
            {
                builder.Clear();
                //TODO 1.2: use append overload
                builder.Append(s);
            }, "Append overload", _count);

            Console.WriteLine();
        }

        public static void CompareDataTypes()
        {
            const int nrOfAppends = 100;

            Console.WriteLine("Int vs. char: ");

            Benchmarker.Benchmark(() =>
            {
                StringBuilder s = new StringBuilder(1000);
                for (int v = 0; v < nrOfAppends; v++)
                {
                    s.Append(1);
                }
            }, "Int", _count, _count*nrOfAppends);

            Benchmarker.Benchmark(() =>
            {
                StringBuilder s = new StringBuilder(1000);
                for (int v = 0; v < nrOfAppends; v++)
                {
                    s.Append('1');
                }
            }, "Char", _count, _count * nrOfAppends);

            //Char is faster (string is an array of chars)

            Console.WriteLine();

            Console.WriteLine("Bool vs. string: ");

            Benchmarker.Benchmark(() =>
            {
                StringBuilder s = new StringBuilder(4000);
                for (int v = 0; v < nrOfAppends; v++)
                {
                    s.Append(true);
                }
            }, "Bool", _count, _count * nrOfAppends);

            Benchmarker.Benchmark(() =>
            {
                StringBuilder s = new StringBuilder(4000);
                for (int v = 0; v < nrOfAppends; v++)
                {
                    s.Append("True");
                }
            }, "String", _count, _count * nrOfAppends);

            //String is faster (bool is a value type, and has to be converted to a string, which is a reference type
            //=> boxing occurs => performance hit)

            Console.WriteLine();

            Console.WriteLine("String vs. char: ");

            Benchmarker.Benchmark(() =>
            {
                StringBuilder s = new StringBuilder(1000);
                for (int v = 0; v < nrOfAppends; v++)
                {
                    s.Append("a");
                }
            }, "String", _count, _count * nrOfAppends);

            Benchmarker.Benchmark(() =>
            {
                StringBuilder s = new StringBuilder(1000);
                for (int v = 0; v < nrOfAppends; v++)
                {
                    s.Append('a');
                }
            }, "Char", _count, _count * nrOfAppends);

            //Char is faster (string is a char array + char is a value type and string is a reference type)
            Console.WriteLine();
        }

        public static void DetermineStringBuilderReusagePerformance()
        {
            string[] stringArray = DataGenerator.GetRandomStringArray(10);

            Benchmarker.Benchmark(() =>
            {
                StringBuilder builder = new StringBuilder();
                foreach (string str in stringArray)
                    builder.Append(str);
                var string1 = builder.ToString();
            }, "Without StringBuilder reusage", _count);

            Benchmarker.Benchmark(() =>
            {
                //TODO 1.4: Use static builder instead of new()
                StringBuilder builder = new StringBuilder();
                builder.Clear();
                foreach (string value in stringArray)
                    builder.Append(value);
                var string2 = builder.ToString();
            }, "With StringBuilder reusage", _count);

            Console.WriteLine();
        }

        public static void ShowStringBuilderConcatenationPerformance()
        {
            string[] stringArray = DataGenerator.GetRandomStringArray(5);

            Benchmarker.Benchmark(() =>
            {
                StringBuilder builder = _builder;
                builder.Clear();
                foreach (string value in stringArray)
                    builder.Append($"ABC {_count}"
                        + "DEF"
                        + string.Concat("GHI", "XYZ")
                        + value);
                var str = builder.ToString();
            }, "With concats in append", _count);

            string a = string.Empty;
            Benchmarker.Benchmark(() =>
            {
                StringBuilder builder = _builder;
                builder.Clear();
                foreach (string value in stringArray)
                {
                    a = $"ABC {_count}"
                        + "DEF"
                        + string.Concat("GHI", "XYZ")
                        + value;
                    builder.Append(a);
                }
                var str = builder.ToString();
            }, "Without concats in appends", _count);

            Console.WriteLine();
        }


        public static void ShowStringBuilderEqualsMistake()
        {
            /*
             * It's not advisable to use the StringBuilder Equals() method to compare strings.
             * Just use the ToString() extension method, and compare both results
             * In order to make equals return true, two StringBuilder objects must have:
             *  - the same capacity
             *  - the same MaxCapacity
             *  - the same characters in their buffers
            */

            string s = DataGenerator.GetRandomString();

            var builder1 = new StringBuilder();
            builder1.Append(s);

            var builder2 = new StringBuilder();
            builder2.Append(s);

            Console.WriteLine($"Equals with new StringBuilder: {builder1.Equals(builder2)}");
            Console.WriteLine($"== with new StringBuilder: {(builder1==builder2)}");
            Console.WriteLine();

            builder1 = new StringBuilder(5000);
            builder1.Append(s);

            builder2 = new StringBuilder(10000);
            builder2.Append(s);

            Console.WriteLine($"Equals with new StringBuilder with different max capacity: {builder1.Equals(builder2)}");
            Console.WriteLine($"== with new StringBuilder with different max capacity: {(builder1 == builder2)}");
            Console.WriteLine();

            var builder3 = _builder;
            builder3.Append(s);

            var builder4 = _builder;
            builder4.Append(s);

            Console.WriteLine($"Equals with static StringBuilder: {builder3.Equals(builder4)}");
            Console.WriteLine($"== with static StringBuilder: {(builder3 == builder4)}");
            Console.WriteLine();

            Console.WriteLine($"Equals with ToString(): {builder1.ToString().Equals(builder2.ToString())}");
            Console.WriteLine($"== with ToString(): {(builder1.ToString() == builder2.ToString())}");
            Console.WriteLine();
        }
    }
}
