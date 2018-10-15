using PerformanceConsoleApp._Shared;
using System;

namespace PerformanceConsoleApp
{
    public static class BranchPerformanceTester
    {
        const int _count = 100000;
        public static void DemoCommonSenseIfOrdering()
        {
            int count = 9999;
            int amountOfDigits = 0;

            Benchmarker.Benchmark(() =>
            {
                for (int c = 0; c < count; c++)
                {
                    if (c < 10)
                    {
                        amountOfDigits = 1;
                    }
                    else if (c < 100)
                    {
                        amountOfDigits = 2;
                    }
                    else if (c < 1000)
                    {
                        amountOfDigits = 3;
                    }
                    else
                    {
                        amountOfDigits = 4;
                    }
                }
            }, "Regular if");

            Benchmarker.Benchmark(() =>
            {
                for (int c = 0; c < count; c++)
                {
                    //TODO 1.27: reorder if statement so amountOfDigits = 4 is in the first if statement
                    if (c < 10)
                    {
                        amountOfDigits = 1;
                    }
                    else if (c < 100)
                    {
                        amountOfDigits = 2;
                    }
                    else if (c < 1000)
                    {
                        amountOfDigits = 3;
                    }
                    else
                    {
                        amountOfDigits = 4;
                    }
                }
            }, "Reordered if");

            Console.WriteLine();

            /*
             * Summary:
             * Since our count is 9999, the else statement in the 1st example would result true more frequently than the other cases
             * In the 2nd example, our first if statement will evaluate to true the most (9000 of our 9999 items are greater than 999)
             * Reordening if statements so more frequent cases are at the top op the if-else statement improves performance, since less
             * if statements have to be evaluated. 
             */
        }

        public static void DemoLoopIncrementVsDecrement()
        {
            int result = 0;

            Benchmarker.Benchmark(() =>
            {
                for (int i = 0; i < _count; i++)
                {
                    result = i;
                }
            }, "Increment", 1, _count);

            result = 0;

            Benchmarker.Benchmark(() =>
            {
                for (int i = _count - 1; i >= 0; i--)
                {
                    result = i;
                }
            }, "Decrement", 1, _count);

            Console.WriteLine();

            /*
             * Summary: 
             * Testing against zero (decrementing) could be a lot faster on your system
             * (possible explanation: processor instruction optimizations).
             */
        }

        public static void DemoForVsForEach()
        {
            var intArray = DataGenerator.GetRandomIntArray(20);

            Benchmarker.Benchmark(() =>
            {
                int a = 0;
                foreach (int value in intArray)
                {
                    a += value;
                }
            }, "Foreach with 1x item access / iteration", 1, intArray.Length);

            Benchmarker.Benchmark(() =>
            {
                int a = 0;
                for (int i = intArray.Length - 1; i >= 0; i--)
                {
                    a += intArray[i];
                }
            }, "For with 1x array access / iteration", 1, intArray.Length);

            Benchmarker.Benchmark(() =>
            {
                int a = 0;
                for (int i = intArray.Length - 1; i >= 0; i--)
                {
                    a += intArray[i];
                    a += intArray[i];
                }
            }, "For with 2x array access / iteration", 1, intArray.Length);

            Benchmarker.Benchmark(() =>
            {
                int a = 0;
                foreach (int value in intArray)
                {
                    a += value;
                    a += value;
                }
            }, "Foreach with 2x item access / iteration", 1, intArray.Length);

            Console.WriteLine();

            /*
             * Summary: a for loop is usually faster if the array only has to be accessed once per iteration
             * Foreach uses a local variable to save the value of the array element, 
             * which increases performance when this value has to be accessed multiple times.
             * This also explains the delay when an item only has to be accessed once (allocation of the local variable)
             */
        }

        public static void DemoIfVsSwitch()
        {
            int testValue1 = 100;

            Benchmarker.Benchmark(() =>
            {
                if (testValue1 == 0) { }
                else if (testValue1 == 1) { }
                else if (testValue1 == 2) { }
                else if (testValue1 == 3) { }
                else if (testValue1 == 4) { }
                else if (testValue1 == 5) { }
                else if (testValue1 == 6) { }
                else { }
            }, "If-else chain of 7 statements", _count);

            Benchmarker.Benchmark(() =>
            {
                switch (testValue1)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    default:
                        break;
                }
            }, "Switch 7 cases", _count);

            Benchmarker.Benchmark(() =>
            {
                switch (testValue1)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    default:
                        break;
                }
            }, "Switch 3 cases", _count);

            Benchmarker.Benchmark(() =>
            {
                if (testValue1 == 0) { }
                else if (testValue1 == 1) { }
                else { }
            }, "If-else chain of 3 statements", _count);

            Console.WriteLine();

            /*
             * Summary:Switch statements are recommended for 7 cases & more
             * However, if the input is almost always a specific value, then using an if-statement to test for that value may be faster.
             */
        }
    }
}
