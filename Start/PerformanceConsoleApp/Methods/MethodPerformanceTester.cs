using PerformanceConsoleApp._Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace PerformanceConsoleApp
{
    public static class MethodPerformanceTester
    {
        const int _count = 100000;
        static int _staticInt = 0;

        public static void DemoOutVsReturn()
        {
            Benchmarker.Benchmark(() =>
            {
                StopwatchWrapper.Time(() =>
                {
                }, out var sw1);
            }, "Out parameter", _count);

            Benchmarker.Benchmark(() =>
            {
                var sw2 = StopwatchWrapper.Time(() =>
                {
                });
            }, "Return", _count);

            Console.WriteLine();
        }

        public static void DemoNestedVsSequential()
        {
            Benchmarker.Benchmark(() =>
            {
                Nested1();
            }, "Nested", _count);

            Benchmarker.Benchmark(() =>
            {
                Sequential1();
            }, "Sequential", _count);

            Console.WriteLine();

            //Summary: deeper call stack should be less performant, however the difference seems negligible
        }

        private static void Nested1() { Nested2(); }
        private static void Nested2() { Nested3(); }
        private static void Nested3() { }

        private static void Sequential1()
        {
            Sequential2();
            Sequential3();
        }
        private static void Sequential2() { }
        private static void Sequential3() { }

        public static void DemoRecursiveInline()
        {
            Benchmarker.Benchmark(() =>
            {
                List<int> list = new List<int>();
                ExecuteRecursiveMethod(list, 0);
            }, "Recursive", _count);

            Benchmarker.Benchmark(() =>
            {
                List<int> list = new List<int>();
                if (list.Count < 10)
                {
                    list.Add(0);
                    ExecuteRecursiveMethod(list, 0 + 1);
                }
            }, "Recursive inline", _count);

            Console.WriteLine();
            //Summary: deeper call stack = less performant
        }

        private static void ExecuteRecursiveMethod(List<int> list, int value)
        {
            if (list.Count < 10)
            {
                list.Add(value);
                ExecuteRecursiveMethod(list, value + 1);
            }
        }

        public static void DemoMultipleReturnValues()
        {
            Benchmarker.Benchmark(() =>
            {
                DataGenerator.GetMultipleStrings(out var string1, out var string2);
            }, "Multiple out parameters", _count);

            Benchmarker.Benchmark(() =>
            {
                DataGenerator.GetMultipleStringsAsKeyValuePair();
            }, "KeyValuePair", _count);

            Benchmarker.Benchmark(() =>
            {
                DataGenerator.GetMultipleStringsAsValueTuple();
            }, "ValueTuple", _count);

            Console.WriteLine();
            /*
             * Summary: out parameters are a lot slower than KeyValuePairs. 
             * KeyValuePairs and tuple performance is almost equal
             * However, tuples are a better practice:
             *   - it allows more than 2 return parameters
             *   - it allows naming each return parameter
             *  Naming:
             *   2-tuple is called a pair.
             *   3-tuple is called a triple.
             *   4-tuple is called a quadruple.
             *   ...
             *   Tuples > 7 are called n-tuples.
             */
        }

        public static void DemoStaticFieldVsLocalVariable()
        {
            Benchmarker.Benchmark(() =>
            {
                _staticInt++;
            }, "Static field ++", _count);

            _staticInt = 0;

            Benchmarker.Benchmark(() =>
            {
                int localInt = _staticInt;
                for (int c = 0; c < _count; c++)
                {
                    localInt++;
                }
                _staticInt = localInt;
            }, "Local variable ++", 1, _count);

            _staticInt = 0;

            Console.WriteLine();

            /* 
             * Summary:
             * Stack vs. heap => local value type variables are stored on the stack
             * Fields (stored on heap) are slower to access.
             */
        }

        public static void DemoInlining()
        {
            //Inlining: optimization by which a method call is replaced with the method body

            Benchmarker.Benchmark(() =>
            {
                NoInliningMethod();
            }, "No inlining", 1);

            Benchmarker.Benchmark(() =>
            {
                DefaultInliningMethod();
            }, "Default inlining", 1);

            Benchmarker.Benchmark(() =>
            {
                AggressiveInliningMethod();
            }, "Aggressive inlining", 1);

            Console.WriteLine();

            Benchmarker.Benchmark(() =>
            {
                NoInliningMethod();
            }, "No inlining loop", _count);

            Benchmarker.Benchmark(() =>
            {
                DefaultInliningMethod();
            }, "Default inlining loop", _count);

            Benchmarker.Benchmark(() =>
            {
                AggressiveInliningMethod();
            }, "Aggressive inlining loop", _count);

            Console.WriteLine();

            /*
             * Summary: If a method is only called in a single place in a program, it MAY help to inline it agressively.
             * However, if a method is called in many places, inlining it may ruin performance due to messed up references
             * (this is especially the case for larger methods)
             * 
             * You might think this is helpful when using private methods in a class only once, however:
             *  - if another developer reuses this method in the future, your program's performance may become negatively impacted
             *  - if the code that calls your method is executed multiple times, default inlining could actually be faster
             *  - results are mixed, depending on the underlying code
             *  
             *  Conclusion: DO NOT USE THIS OR YOU/SOMEONE ELSE WILL PROBABLY CAUSE THE PROGRAM TO SLOW DOWN AT SOME POINT IN TIME
             */
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static int NoInliningMethod()
        {
            return 8 + 5 - 3 + 7 + 6 * 3 - 2 * 8 + 9 + 9 + "blablablabla".Length - 3 + 7 + 6 * 3 - 2 * 8;
        }

        private static int DefaultInliningMethod()
        {
            return 8 + 5 - 3 + 7 + 6 * 3 - 2 * 8 + 9 + 9 + "blablablabla".Length - 3 + 7 + 6 * 3 - 2 * 8;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int AggressiveInliningMethod()
        {
            return 8 + 5 - 3 + 7 + 6 * 3 - 2 * 8 + 9 + 9 + "blablablabla".Length - 3 + 7 + 6 * 3 - 2 * 8;
        }
    }
}
