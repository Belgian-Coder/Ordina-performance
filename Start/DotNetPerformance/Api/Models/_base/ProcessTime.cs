using System.Diagnostics;

namespace DotNetPerformance.Api.Models._base
{
    public class TimedProcess<T>
    {
        private Stopwatch Watch { get; set; }

        public string TimeInMilliseconds
        {
            get => Watch.Elapsed.TotalMilliseconds.ToString("N");
        }
        public T Result { get; set; }

        public TimedProcess()
        {
            Watch = new Stopwatch();
        }

        public void StartTimer()
        {
            Watch.Start();
        }

        public void StopTimer()
        {
            Watch.Stop();
        }
    }
}
