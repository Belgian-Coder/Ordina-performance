using PerformanceConsoleApp._Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PerformanceConsoleApp
{
    public static class BoxingPerformanceTester
    {
        private const int _count = 100000;
        public static void ExecuteDemoBoxingVsGenerics()
        {
            var randomData = DataGenerator.GetRandomIntArray(3);
            ArrayList objectList = new ArrayList(randomData.ToList());
            List<int> intList = randomData.ToList();
            int newNr = DataGenerator.GetRandomInt();

            Benchmarker.Benchmark(() =>
            {
                //With boxing (the wrong way):
                objectList.Add(newNr); // boxing required
            }, "Using arrayList(boxing required)", _count);

            Benchmarker.Benchmark(() =>
            {
                //Without boxing (the right way):
                intList.Add(newNr);
            }, "Using generics (no boxing required)", _count);

            Console.WriteLine();

            Benchmarker.Benchmark(() =>
            {
                //With unboxing (the wrong way):
                int firstValue = (int)objectList[0]; // unboxing & casting required

            }, "Using arrayList (unboxing & casting required)", _count);
         
            Benchmarker.Benchmark(() =>
            {
                //Without unboxing (the right way):
                int orderId = intList[0];  // unboxing & casting not required
            }, "Using generics (unboxing & casting not required)", _count);
         
            Console.WriteLine();

            Benchmarker.Benchmark(() =>
            {
                //With (un)boxing (the wrong way):
                objectList.Add(newNr); // boxing required
                int firstValue = (int)objectList[0]; // unboxing & casting required
            }, "Using arrayList (boxing, unboxing & casting required)", _count);

            Benchmarker.Benchmark(() =>
            {
                //Without (un)boxing (the right way):
                intList.Add(newNr);
                int orderId = intList[0];  // unboxing & casting not required
            }, "Using generics (no boxing, unboxing & casting required)", _count);

            Console.WriteLine();
        }
    }
}
