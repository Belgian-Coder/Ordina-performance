using PerformanceConsoleApp._Shared;
using System;
using System.Collections.Generic;

namespace PerformanceConsoleApp
{
    public static class ClassStructPerformanceTester
    {
        const int _count = 100000;
        public static void DemoClassVsStruct()
        {
            string str = DataGenerator.GetRandomString();
            int i = DataGenerator.GetRandomInt();

            Console.WriteLine("Initialization:");

            Benchmarker.Benchmark(() =>
            {
                var a = new SomeClassWithStrings
                {
                    A = str
                };
            }, "Class with string", _count);

            Benchmarker.Benchmark(() =>
            {
                SomeStructWithStrings a = new SomeStructWithStrings
                {
                    A = str
                };
            }, "Struct with string", _count);
           
            Benchmarker.Benchmark(() =>
            {
                var a = new SomeClassWithInts
                {
                    A = i
                };
            }, "Class with int", _count);

            Benchmarker.Benchmark(() =>
            {
                SomeStructWithInts a = new SomeStructWithInts
                {
                    A = i
                };
            }, "Struct with int", _count);

            Console.WriteLine();


            Console.WriteLine("As a method parameter:");

            var pair1 = new KeyValuePair<string, SomeClassWithStrings>("key",new SomeClassWithStrings());
            var pair2 = new KeyValuePair<string, SomeStructWithStrings>("key",new SomeStructWithStrings());

            Benchmarker.Benchmark(() =>
            {
                var a = pair2.Value;
            }, "Struct as parameter", _count);

            Benchmarker.Benchmark(() =>
            {
                var a = pair1.Value;
            }, "Class as parameter", _count);

            /*
             * Summary: Allocating structs is generally faster than allocating classes,
             * however it's not recommended to use structs with a lot of fields,
             * since the struct and all of it's fields will be copied onto the function stack when using structs as a parameter
             */
        }

        private class SomeClassWithStrings
        {
            public string A { get; set; }
            public string B { get; set; }
            public string C { get; set; }
            public string D { get; set; }
            public string E { get; set; }
            public string F { get; set; }
            public string G { get; set; }
            public string H { get; set; }
        }

        private struct SomeStructWithStrings
        {
            public string A, B, C, D, E, F, G, H;
        }

        private class SomeClassWithInts
        {
            public int A { get; set; }
            public int B { get; set; }
            public int C { get; set; }
            public int D { get; set; }
            public int E { get; set; }
            public int F { get; set; }
            public int G { get; set; }
            public int H { get; set; }
        }

        private struct SomeStructWithInts
        {
            public int A, B, C, D, E, F, G, H;

            public SomeStructWithInts(int a, int b)
            {
                A = a;
                B = b;
                C = 0;
                D = 0;
                E = 0;
                F = 0;
                G = 0;
                H = 0;
            }
        }
    }
}
