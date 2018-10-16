using PerformanceConsoleApp._Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PerformanceConsoleApp
{
    public static class DictionaryPerformanceTester
    {
        const int _count = 10000000;

        public static void DemoKeyLengthPerformance()
        {
            Dictionary<string, int> _dict = new Dictionary<string, int> {
                { DataGenerator.GetRandomString(), DataGenerator.GetRandomInt(1,9)},
                { DataGenerator.GetRandomString(), DataGenerator.GetRandomInt(1,9)},
                { DataGenerator.GetRandomString(), DataGenerator.GetRandomInt(1,9)},
            };

            string key0 = "00000000000000000001";
            //TODO 1.13: try this with shorter keys

            Benchmarker.Benchmark(() =>
            {
                _dict.ContainsKey(key0);
            }, "ContainsKey(20 chars)", _count);

            Benchmarker.Benchmark(() =>
            {
                _dict.ContainsKey(key0);
            }, "ContainsKey(10 chars)", _count);

            Benchmarker.Benchmark(() =>
            {
                _dict.ContainsKey(key0);
            }, "ContainsKey(5 chars)", _count);

            Benchmarker.Benchmark(() =>
            {
                _dict.ContainsKey(key0);
            }, "ContainsKey(1 char)", _count);

            Console.WriteLine();
        }

        public static void DemoMemoization()
        {
            List<string> s = DataGenerator.GetRandomStringArray(10).ToList();
            string result = string.Empty;
            Dictionary<string, string> _upperCaseTempDictionary = new Dictionary<string, string>();

            Benchmarker.Benchmark(() =>
            {
                foreach (var item in s)
                {
                    result = item.ToUpper();
                }
            }, "ToUpper()", _count);

            Benchmarker.Benchmark(() =>
            {
                foreach (var item in s)
                {
                    if (_upperCaseTempDictionary.TryGetValue(item, out string lookupValue))
                    {
                        result = lookupValue;
                        continue;
                    }
                    lookupValue = item.ToUpper();
                    _upperCaseTempDictionary[item] = lookupValue;
                }
            }, "ToUpper() with lookup dictionary (memoization)", _count);

            /*
              * Memoization is a way of caching used to optimize a function.
              * Known results are stored in memory, so the computation only runs once
              * 
              * NOTE: the heavier a computation, the more useful this gets.
              * Don't forget that Dictionaries will become slower as they increase in size!
              * The following example would cause a performance hit when using a very big stringarray
              * 
              * Alternative: inject IMemoryCache
              */

            Console.WriteLine();
        }

        public static void DemoTryGetValueVsContainsKey()
        {
            var counts = new Dictionary<string, int> { { "key", DataGenerator.GetRandomInt() }};
            int result;

            Benchmarker.Benchmark(() =>
            {
                if (counts.ContainsKey("key"))
                {
                    result = counts["key"];
                }
            }, "ContainsKey()", _count);

            Benchmarker.Benchmark(() =>
            {
                if (counts.TryGetValue("key", out result))
                {
                }
            }, "TryGetValue()", _count);

            //Summary: use TryGetValue over ContainsKey, since ContainsKey causes an unnecessary lookup

        }
    }
}
