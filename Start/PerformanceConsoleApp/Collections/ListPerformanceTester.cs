using PerformanceConsoleApp._Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PerformanceConsoleApp
{
    public static class ListPerformanceTester
    {
        private const int _count = 100000;
        public static void Count()
        {
            List<int> list = DataGenerator.GetRandomIntArray(10).ToList();
            int c = 0;

            Benchmarker.Benchmark(() =>
            {
                //With extension method (the wrong way):
                c = list.Count();
            }, "Using extension method Count()", _count);

            Benchmarker.Benchmark(() =>
            { 
                //TODO 1.10: With property (the right way):
                c = list.Count();
            }, "Using property Count", _count);

            Console.WriteLine();
        }

        public static void DemoCustomContains()
        {
            List<int> list = DataGenerator.GetRandomIntArray(10, 10, 10000).ToList();
            bool itemFound = false;

            Benchmarker.Benchmark(() =>
            {
                itemFound = list.Contains(5);
            }, "Using extension method Contains()", _count);

            Benchmarker.Benchmark(() =>
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] == 5)
                    {
                        //found item, stop searching
                        itemFound = true;
                        break;
                    }
                }
                itemFound = false;
            }, "Using custom Contains method", _count);

            Console.WriteLine();
        }

        public static void DemoRemoveListVsRemoveDictionary()
        {
            List<int> list = DataGenerator.GetRandomIntArray(_count).ToList();
            var listCopy = list.ToList();
            Dictionary<int, int> dictionary = DataGenerator.GetRandomIntDictionary(_count, 1, 1000000000);
            var dictionaryCopy = dictionary.ToDictionary(x => x.Key, x => x.Value);

            Benchmarker.Benchmark(() =>
            {
                foreach (var item in listCopy)
                {
                    list.Remove(item);
                }
            }, "List removal", 1, _count);

            Benchmarker.Benchmark(() =>
            {
                foreach (var item in dictionaryCopy)
                {
                    dictionary.Remove(item.Key);
                }
            }, "Dictionary removal", 1, _count);

            /* 
             * A List is a contiguous collection of elements. 
             * When you remove an element from it, all following elements must be copied forward. 
             * This requires allocations and computations. 
             * For large Lists, this can be slow
             */

            Console.WriteLine();
        }
    }
}
