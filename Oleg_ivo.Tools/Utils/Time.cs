using System;
using System.Diagnostics;

namespace Oleg_ivo.Tools.Utils
{
    public class ElapsedAction : IDisposable
    {
        private Action<TimeSpan> postAction;
        private Stopwatch stopwatch;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public ElapsedAction(Action<TimeSpan> postAction)
        {
            this.postAction = postAction;
            stopwatch = Stopwatch.StartNew();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (stopwatch == null) return;
            stopwatch.Stop();
            postAction(stopwatch.Elapsed);
            stopwatch = null;
            postAction = null;
        }
    }
}
