using System;
using System.Diagnostics;

namespace PerformanceConsoleApp._Shared
{
    public static class StopwatchWrapper
    {
        public static void Time(Action action, out Stopwatch sw)
        {
            sw = new Stopwatch();
            sw.Start();
            action();
            sw.Stop();
        }

        public static Stopwatch Time(Action action)
        {
            var sw = new Stopwatch();
            sw.Start();
            action();
            sw.Stop();
            return sw;
        }
    }
}
