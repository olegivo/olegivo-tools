using System;
using System.Reactive.Subjects;
using NLog;
using NLog.Targets;

namespace Oleg_ivo.Base.WPF.LogBinding
{
    [Target("ObservableTarget")]
    public class ObservableLogTarget :TargetWithLayout, IObservable<RenderedLogEvent>
    {
        private ISubject<RenderedLogEvent> subject;

        private const int DefaultReplayBufferSize = 100;

        public int? ReplayBufferSize { get; set; }

        protected override void InitializeTarget()
        {
            this.subject = new ReplaySubject<RenderedLogEvent>(bufferSize: ReplayBufferSize ?? DefaultReplayBufferSize);
            base.InitializeTarget();
        }

        protected override void Write(LogEventInfo logEvent)
        {
            var message = this.Layout.Render(logEvent);
            subject.OnNext(new RenderedLogEvent(logEvent, message));
        }


        public IDisposable Subscribe(IObserver<RenderedLogEvent> observer)
        {
            return subject.Subscribe(observer);
        }

    }
}
