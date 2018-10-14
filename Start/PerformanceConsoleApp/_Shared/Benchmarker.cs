using System;
using System.Diagnostics;

namespace PerformanceConsoleApp._Shared
{
    public static class Benchmarker
    {
        public static void Benchmark(Action action, string actionName, int executionCount = 1, int executionLogCount = 0)
        {
            if (executionLogCount <= 0) executionLogCount = executionCount;

            var sw = new Stopwatch();
            sw.Start();
            for (int i = executionCount - 1; i >= 0; --i)
            {
                action();
            }
            sw.Stop();

            if (executionLogCount > 0)
                Console.WriteLine($"{actionName}: " + (sw.Elapsed.TotalMilliseconds * 1000000 / executionLogCount).ToString("0.00 ns"));
        }
    }
}
