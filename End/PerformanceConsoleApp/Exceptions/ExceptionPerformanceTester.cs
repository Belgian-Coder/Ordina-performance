using PerformanceConsoleApp._Shared;
using System;

namespace PerformanceConsoleApp
{
    public static class ExceptionPerformanceTester
    {
        const int _count = 100000;
        public static void DemoTryCatchInnerLoop()
        {
            Benchmarker.Benchmark(() =>
            {
                for (int c = 0; c < _count; c++)
                {
                    try
                    {
                        //inner is slower
                        if (c < 0) throw new Exception();
                        //no difference if u would change the previous statement by the following line of code:
                        //if (c < 0) return;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }, "Inline try catch", 1, _count);

            Benchmarker.Benchmark(() =>
            {
                //TODO 1.26: move try catch
                for (int c = 0; c < _count; c++)
                {
                    try
                    { //outer is faster
                        if (c < 0) throw new Exception();
                        //no difference if u would change the previous statement by the following line of code:
                        //if (c < 0) return;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }, "Outer try catch", 1, _count);

            Console.WriteLine();

            /*
             * Summary: if we try this with code that can't throw an exception inside the try catch,
             * it doesn't seem to matter if the try catch statement is located inside or outside of the loop
             * However, from the moment an exception could occur, 
             * it's significantly faster to wrap your try catch statement around the loop
             * 
             * (test this yourself, check the comments in code)
             */
        }
    }
}
