using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.Diagnostics
{
    public class TimeSplice
    {
        private Stopwatch _StopWatch { get; set; }

        public string Name { get; private set; }
        public Int64 TotalMilliseconds { get; private set; }
        public DateTime Started { get; private set; }

        /// <summary>
        /// Create a new timesplice.
        /// </summary>
        /// <param name="name">Name of the timesplice.</param>
        public TimeSplice(string name) : this(name, false) { }

        /// <summary>
        /// Create a new timesplice.
        /// </summary>
        /// <param name="name">Name of the timesplice.</param>
        /// <param name="start">Start recording time automatically.</param>
        public TimeSplice(string name, bool start)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");

            _StopWatch = new Stopwatch();

            if (start)
                Start();
        }

        /// <summary>
        /// Start recording time in the timepslice.
        /// </summary>
        public void Start()
        {
            if (_StopWatch == null)
                throw new NullReferenceException("Stopwatch is null");

            if (_StopWatch.IsRunning)
                throw new Exception("Stopwatch is already running.");

            Started = DateTime.UtcNow;
            _StopWatch.Start();
        }

        /// <summary>
        /// Stop recording time in the timesplice.
        /// </summary>
        public void Stop()
        {
            if (_StopWatch == null)
                throw new NullReferenceException("Stopwatch is null");

            if (!_StopWatch.IsRunning)
                throw new Exception("Stopwatch is not running.");

            _StopWatch.Stop();
            TotalMilliseconds = _StopWatch.ElapsedMilliseconds;

            _StopWatch = null;
        }
    }
}